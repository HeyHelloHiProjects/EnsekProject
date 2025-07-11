using Application.Common.Interfaces;
using CsvHelper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace Application.Features.UserMeterProccessing.Commands;

public class AddUserMetersCommand : IRequest<ReadingRateDataViewModel>
{
    public string? CsvData { get; set; }
}

public class AddUserMetersCommandHandler : IRequestHandler<AddUserMetersCommand, ReadingRateDataViewModel>
{
    private readonly IApplicationDbContext _context;

    public AddUserMetersCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReadingRateDataViewModel> Handle(AddUserMetersCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.CsvData))
        {
            return new ReadingRateDataViewModel();
        }

        var records = GetMeterDatas(request.CsvData);

        var recordsId = records.Select(data => data.AccountId).ToList();
        var existingAccountIds = await _context.UserAccounts
                                            .Where(user => recordsId.Contains(user.AccountId))
                                            .Select(user => user.AccountId)
                                            .ToListAsync(cancellationToken);

        var convertedRecordsToSave = new List<MeterReadingData>(existingAccountIds.Count);
        var validRecordsById = records.Where(record => existingAccountIds.Contains(record.AccountId));
        foreach (var record in validRecordsById)
        {
            // Since we have pretty unclear requirements in the initial task
            var isReadMeterValue = int.TryParse(record.MeterReadValue, out var meterReadValue);
            var isReadMeterDateTime = DateTime.TryParse(record.MeterReadingDateTime, out var meterReadingDateTime);
            if (!isReadMeterValue || !isReadMeterDateTime)
            {
                continue;
            }

            // Since we have pretty unclear requirements in the initial task
            var convertedRecord = new MeterReadingData
            {
                AccountId = record.AccountId,
                MeterReadingDateTime = meterReadingDateTime,
                MeterReadValue = meterReadValue,
            };
            convertedRecordsToSave.Add(convertedRecord);
        }

        await _context.MetersReadingData.AddRangeAsync(convertedRecordsToSave, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ReadingRateDataViewModel
        {
            FailedRowCount = records.Length - convertedRecordsToSave.Count,
            SuccessfulRowCount = convertedRecordsToSave.Count
        };
    }

    private static MeterData[] GetMeterDatas(string csvData)
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvData));
        using var csv = new CsvReader(new StreamReader(stream), CultureInfo.InvariantCulture);

        return csv.GetRecords<MeterData>().ToArray();
    }

    // Since we have pretty unclear requirements in the initial task
    private class MeterData
    {
        public int AccountId { get; set; }
        public string MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
    }
}

