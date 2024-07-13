# Maniceraf.Hangfire.JobChecker

## Description:

The Hangfire Job Checker Filter library provides a custom Hangfire filter (JobCheckerFilter) that allows you to execute custom logic after a Hangfire job has been performed. This filter integrates with Hangfire's server-side processing to execute an IJobChecker service asynchronously, allowing for post-job checks or actions. This setup facilitates error handling, logging, or additional operations after each job execution within your Hangfire background processing setup.

## Features:

  - Custom Job Checking: Execute custom logic after each Hangfire job using the JobCheckerFilter.
  - Integration with Dependency Injection: Supports dependency injection for IJobChecker implementations.

## Usage:

Register services in your DI container:

```csharp
builder.Services.AddScoped(typeof(IEmailService), typeof(EmailService));
builder.Services.AddHangfireJobHunter<JobHunterDemo>();
```

Create service and custom logic after each Hangfire job, My example below is to create a service that sends warning emails to failed jobs.

```csharp
public class JobHunterDemo : IJobHunter
{
    private readonly IEmailService _emailService;
    public JobHunterDemo(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [Obsolete]
    public async Task ExecuteAync(PerformedContext context)
    {
        if (context.Exception != null)
        {
            var title = $"Hangfire Job Failed - Job #: {context.JobId}";
            var div = new DivTag();
            div.AddChildren(new H4Tag($"Server #: {context.ServerId}"));
            div.AddChildren(new H4Tag($"Job #: {context.JobId}"));
            div.AddChildren(new H4Tag($"Job: {context.Job}"));
            div.AddChildren(new H4Tag($"Created At: {context.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss tt")}"));
            var a = new H4Tag($"Exception: {context.Exception.InnerException?.Message}. {context.Exception.InnerException?.StackTrace}");
            a.AddAttribute("style", "color:red");
            div.AddChildren(a);

            var document = new HtmlDocument();
            document.Body.AddChildren(div);

            await _emailService.SendEmailAsync("hungnguyen8102000@gmail.com", title, document.ToString());
        }

        await Task.CompletedTask;
    }
}
```
