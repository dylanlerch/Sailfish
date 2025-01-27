﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Sailfish.Contracts.Public;
using Sailfish.Contracts.Public.CsvMaps;
using Sailfish.Statistics;

namespace Sailfish.Presentation.Csv;

internal class PerformanceCsvWriter : IPerformanceCsvWriter
{
    private readonly IFileIo fileIo;

    public PerformanceCsvWriter(IFileIo fileIo)
    {
        this.fileIo = fileIo;
    }

    public async Task Present(IEnumerable<ExecutionSummary> result, string filePath, CancellationToken cancellationToken)
    {
        var writer = new StreamWriter(filePath);
        await using (writer.ConfigureAwait(false))
        {
            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await using (csv.ConfigureAwait(false))
            {
                csv.Context.RegisterClassMap<TestCaseDescriptiveStatisticsMap>();

                foreach (var records in from container in result where container.Settings.AsCsv select container.CompiledResults.Select(x => x.DescriptiveStatisticsResult))
                {
                    await csv.WriteRecordsAsync(records, cancellationToken);
                }
            }
        }

        await Task.CompletedTask;
    }
}