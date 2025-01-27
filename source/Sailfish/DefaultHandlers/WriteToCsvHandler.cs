﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sailfish.Contracts.Private;
using Sailfish.Presentation;
using Sailfish.Presentation.Csv;

namespace Sailfish.DefaultHandlers;

internal class WriteToCsvHandler : INotificationHandler<WriteToCsvCommand>
{
    private readonly IPerformanceCsvWriter performanceCsvWriter;

    public WriteToCsvHandler(IPerformanceCsvWriter performanceCsvWriter)
    {
        this.performanceCsvWriter = performanceCsvWriter;
    }

    public async Task Handle(WriteToCsvCommand notification, CancellationToken cancellationToken)
    {
        var fileName = DefaultFileSettings.AppendTagsToFilename(DefaultFileSettings.DefaultPerformanceFileNameStem(notification.TimeStamp) + ".csv", notification.Tags);
        var filePath = Path.Combine(notification.OutputDirectory, fileName);
        await performanceCsvWriter.Present(notification.Content, filePath, cancellationToken).ConfigureAwait(false);
    }
}