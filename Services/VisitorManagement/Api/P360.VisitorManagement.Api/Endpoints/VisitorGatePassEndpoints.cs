using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using GatePassEntity = P360.VisitorManagement.Domain.Entities.VisitorGatePass;
using GatePassDocumentEntity = P360.VisitorManagement.Domain.Entities.VisitorGatePassDocument;
using Microsoft.AspNetCore.Mvc;
using P360.Email;
using P360.Media.Client;
using P360.Repository.Repositories;
using P360.VisitorManagement.Api.Contracts;
using P360.VisitorManagement.Api.Settings;
using P360.VisitorManagement.Api.Validation;
using P360.VisitorManagement.Domain.Entities;
using P360.VisitorManagement.Repository.Repositories;

namespace P360.VisitorManagement.Api.Endpoints;

public static class VisitorGatePassEndpoints
{
    private const string GatePassPermitType = "Visitor Permit";
    private const string StageRequestTemplateName = "gatepass_approval_request_mail";
    private const string ApprovedTemplateName = "gatepass_approval_mail";
    private const string RejectedTemplateName = "gatepass_rejection_mail";

    private static readonly Dictionary<string, (string CanonicalType, string Category)> DocumentTypeMap = new()
    {
        ["photo"] = ("Photo", "VisitorGatePassPhoto"),
        ["passporteid"] = ("Passport(EID)", "VisitorGatePassPassport"),
        ["visa"] = ("Visa", "VisitorGatePassVisa"),
        ["supportingdocs"] = ("Supporting Docs", "VisitorGatePassSupportingDocs"),
        ["nationalid"] = ("National ID", "VisitorGatePassNationalId")
    };

    private static readonly string DefaultStageRequestTemplateBody =
        """
        <body>
        <div style="width: 65%; margin: 0 auto; font-family: sans-serif; font-size: 14px;padding:10px 20px;letter-spacing: 0.5px;">
        <table cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="text-align: center;" colspan="2">
                    <img src="https://www.purpleiq.ai/images/logo.png" alt="" style="margin-bottom: 30px;width:300px">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding-bottom: 5px;" colspan="2">
                    <p style="font-size: 18px;font-weight: 300;">Visitor Gate Pass Approval Required</p>
                    <p style="font-size: 18px;font-weight: 300;">Dear {{fullname}},</p>
                    <p style="font-size: 18px;font-weight: 300;">A visitor gate pass request with reference number "{{referenceno}}" requires your approval.</p>
                    <p style="font-size: 16px;font-weight: 300;">
                        Contact Name: {{contactname}}<br>
                        Host: {{hostperson}} ({{hostcompany}})<br>
                        Visitor Company: {{visitorcompany}}<br>
                        Reason of Visit: {{reasonofvisit}}<br>
                        From: {{fromdate}} &nbsp; To: {{todate}}
                    </p>
                    <table cellpadding="0" cellspacing="0" style="margin: 20px 0;">
                        <tr>
                            <td style="padding-right: 12px;">
                                <a href="{{approveurl}}" style="background-color:#28a745;color:#ffffff;padding:10px 24px;text-decoration:none;border-radius:4px;font-weight:bold;">Approve</a>
                            </td>
                            <td>
                                <a href="{{rejecturl}}" style="background-color:#dc3545;color:#ffffff;padding:10px 24px;text-decoration:none;border-radius:4px;font-weight:bold;">Reject</a>
                            </td>
                        </tr>
                    </table>
                    <p style="font-size: 14px;font-weight: 300;">Note : This is an automatically generated email - please do not reply to this message.</p>
                    <p style="font-size: 18px;font-weight: 300;">Regards,</p>
                    <p style="font-size: 18px;font-weight: 300;">Purple IQ team!</p>
                </td>
            </tr>
        </table>
        </div>
        </body>
        """;

    private static readonly string DefaultApprovedTemplateBody =
        """
         <body>
             <div
        style="width: 65%; margin: 0 auto; font-family: sans-serif; font-size: 14px;padding:10px 20px;letter-spacing: 0.5px;">

        <table cellpadding="0" cellspacing="" style="width: 100%;">
            <tr>
                <td style="text-align: center;" colspan="2">
                    <img src="https://www.purpleiq.ai/images/logo.png" alt="" style="margin-bottom: 30px;width:300px">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding-bottom: 5px;" colspan="2">
                    <p style="font-size: 18px;font-weight: 300;">PCFC | Gate Pass Approval Attached</p>
                    <p style="font-size: 18px;font-weight: 300;">Dear Valued User,</p>
                    <p style="font-size: 18px;font-weight: 300;">Your Gate Pass Request with reference number
                        <span>"{{referenceno}}"</span> has been approved.</p>
                    <p style="font-size: 18px;font-weight: 300;">Kindly print attached pass to present at the Gate.</p>
                    <p style="font-size: 18px;font-weight: 300;">To reach the support team, please call
                        <span>{{SuppoertMobileNo}}</span> support on <span>{{support_url}}</span> or by email to <span>{{SupportEmail}}</span></p>
                    <p style="font-size: 18px;font-weight: 300;">We continue to strive in always providing you with our
                        best possible services.</p>
                    <p style="font-size: 18px;font-weight: 300;">Note : This is an automatically generated email -
                        please do not reply to this message.</p>
                    <p style="font-size: 18px;font-weight: 300;">Regards,</p>
                    <p style="font-size: 18px;font-weight: 300;">"{{fullname}}"</p>
                </td>
            </tr>
        </table>

        </div>
        </body>
        """;

    private static readonly string DefaultRejectedTemplateBody =
        """
        <body>
        <div style="width: 65%; margin: 0 auto; font-family: sans-serif; font-size: 14px;padding:10px 20px;letter-spacing: 0.5px;">
        <table cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="text-align: center;" colspan="2">
                    <img src="https://www.purpleiq.ai/images/logo.png" alt="" style="margin-bottom: 30px;width:300px">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding-bottom: 5px;" colspan="2">
                    <p style="font-size: 18px;font-weight: 300;">Dear Valued User,</p>
                    <p style="font-size: 18px;font-weight: 300;">Your Gate Pass Request with reference number "{{referenceno}}" has been rejected.</p>
                    <p style="font-size: 18px;font-weight: 300;">Remarks: {{remarks}}</p>
                    <p style="font-size: 18px;font-weight: 300;">Note : This is an automatically generated email - please do not reply to this message.</p>
                    <p style="font-size: 18px;font-weight: 300;">Regards,</p>
                    <p style="font-size: 18px;font-weight: 300;">"{{fullname}}"</p>
                </td>
            </tr>
        </table>
        </div>
        </body>
        """;

    public static RouteGroupBuilder MapVisitorGatePassEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorgatepasses")
            .WithTags("VisitorGatePasses");

        group.MapGet("", GetAllAsync)
            .WithName("GetVisitorGatePasses");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorGatePassById");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorGatePass");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorGatePass");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorGatePass");

        group.MapPost("/{id}/approve", ApproveAsync)
            .WithName("ApproveVisitorGatePass");

        group.MapPost("/{id}/reject", RejectAsync)
            .WithName("RejectVisitorGatePass");

        group.MapGet("/{id}/approve-by-link", ApproveByLinkAsync)
            .WithName("ApproveVisitorGatePassByLink");

        group.MapGet("/{id}/reject-by-link", RejectByLinkAsync)
            .WithName("RejectVisitorGatePassByLink");

        group.MapPost("/{id}/documents/upload", UploadDocumentAsync)
            .WithName("UploadVisitorGatePassDocument")
            .DisableAntiforgery();

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorGatePassRepository repository,
        CancellationToken cancellationToken)
    {
        var gatePasses = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            gatePasses.Select(VisitorGatePassResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorGatePassRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid gate pass id." });
        }

        var gatePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return gatePass is null
            ? Results.NotFound()
            : Results.Ok(VisitorGatePassResponse.FromEntity(gatePass));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorGatePassRequest request,
        IVisitorGatePassRepository repository,
        IVisitorApprovalRepository approvalRepository,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        GatePassNotificationSettings notificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var approvalConfig = await approvalRepository.GetByPermitTypeAsync(
            GatePassPermitType,
            cancellationToken);

        var precedenceEmails = approvalConfig?.EmployeeEmailIds
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .ToList() ?? [];

        var approverChain = BuildEffectiveApproverChain(request.HostPersonEmail, precedenceEmails);

        var gatePass = request.ToEntity();
        gatePass.AuthCode = GenerateSixDigitCode();
        gatePass.VisitorPassReferenceNo = GenerateReferenceNo();
        gatePass.ApproverChain = approverChain;
        gatePass.MaxApprovalLevel = approverChain.Count;

        if (approverChain.Count == 0)
        {
            gatePass.Status = "Approved";
            gatePass.StatusLevel = 0;
            gatePass.IsLevelProcessed = true;
            gatePass.ProcessedBy = "system";
            gatePass.ProcessedAt = DateTime.UtcNow;
            gatePass.ApprovedBy = "system";
            gatePass.ApprovedOn = DateTime.UtcNow;
            gatePass.ApprovedRemarks = "Auto-approved: no approval chain configured for Visitor Permit.";
        }
        else
        {
            gatePass.Status = "Pending";
            gatePass.StatusLevel = 0;
        }

        var created = await repository.CreateAsync(
            gatePass,
            cancellationToken);

        if (approverChain.Count > 0)
        {
            await SendStageRequestEmailsAsync(
                created,
                0,
                templateRepository,
                emailService,
                notificationSettings,
                loggerFactory,
                cancellationToken);
        }

        return Results.Created(
            $"/api/visitorgatepasses/{created.Id}",
            VisitorGatePassResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorGatePassRequest request,
        IVisitorGatePassRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid gate pass id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var gatePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (gatePass is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(gatePass);

        var updated = await repository.UpdateAsync(
            id,
            gatePass,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorGatePassResponse.FromEntity(gatePass))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorGatePassRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid gate pass id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> ApproveAsync(
        string id,
        ApproveGatePassRequest request,
        IVisitorGatePassRepository repository,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        GatePassNotificationSettings notificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid gate pass id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var gatePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (gatePass is null)
        {
            return Results.NotFound();
        }

        if (!TryApplyDecision(gatePass, request.ApproverEmail!.Trim(), true, request.Remarks, out var errorMessage))
        {
            return Results.BadRequest(new { message = errorMessage });
        }

        await repository.UpdateAsync(
            id,
            gatePass,
            cancellationToken);

        if (gatePass.Status == "Approved")
        {
            await SendFinalDecisionEmailAsync(gatePass, true, templateRepository, emailService, loggerFactory, cancellationToken);
        }
        else
        {
            await SendStageRequestEmailsAsync(
                gatePass,
                gatePass.StatusLevel!.Value,
                templateRepository,
                emailService,
                notificationSettings,
                loggerFactory,
                cancellationToken);
        }

        return Results.Ok(VisitorGatePassResponse.FromEntity(gatePass));
    }

    private static async Task<IResult> RejectAsync(
        string id,
        RejectGatePassRequest request,
        IVisitorGatePassRepository repository,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid gate pass id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var gatePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (gatePass is null)
        {
            return Results.NotFound();
        }

        if (!TryApplyDecision(gatePass, request.ApproverEmail!.Trim(), false, request.Remarks, out var errorMessage))
        {
            return Results.BadRequest(new { message = errorMessage });
        }

        await repository.UpdateAsync(
            id,
            gatePass,
            cancellationToken);

        await SendFinalDecisionEmailAsync(gatePass, false, templateRepository, emailService, loggerFactory, cancellationToken);

        return Results.Ok(VisitorGatePassResponse.FromEntity(gatePass));
    }

    private static async Task<IResult> ApproveByLinkAsync(
        string id,
        string email,
        string token,
        IVisitorGatePassRepository repository,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        GatePassNotificationSettings notificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await HandleLinkDecisionAsync(
            id,
            email,
            token,
            true,
            null,
            repository,
            templateRepository,
            emailService,
            notificationSettings,
            loggerFactory,
            cancellationToken);
    }

    private static async Task<IResult> RejectByLinkAsync(
        string id,
        string email,
        string token,
        IVisitorGatePassRepository repository,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        GatePassNotificationSettings notificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await HandleLinkDecisionAsync(
            id,
            email,
            token,
            false,
            "Rejected via email link",
            repository,
            templateRepository,
            emailService,
            notificationSettings,
            loggerFactory,
            cancellationToken);
    }

    private static async Task<IResult> HandleLinkDecisionAsync(
        string id,
        string email,
        string token,
        bool approve,
        string? remarks,
        IVisitorGatePassRepository repository,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        GatePassNotificationSettings notificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return RenderLinkResultPage("Invalid Link", "This gate pass link is invalid.");
        }

        var gatePass = await repository.GetByIdAsync(id, cancellationToken);

        if (gatePass is null)
        {
            return RenderLinkResultPage("Not Found", "No gate pass was found for this link.");
        }

        if (gatePass.Status is "Approved" or "Rejected")
        {
            return RenderLinkResultPage(
                "Already Processed",
                $"This gate pass has already been {gatePass.Status}. No further action is needed.");
        }

        if (gatePass.ApproverChain.Count == 0 ||
            gatePass.StatusLevel is null ||
            gatePass.StatusLevel >= gatePass.ApproverChain.Count)
        {
            return RenderLinkResultPage("No Pending Approval", "There is no pending approval step for this gate pass.");
        }

        var expectedToken = ComputeLevelToken(id, gatePass.StatusLevel.Value, notificationSettings.LinkSecret);

        if (!string.Equals(expectedToken, token, StringComparison.OrdinalIgnoreCase))
        {
            return RenderLinkResultPage("Invalid or Expired Link", "This approval link is invalid or has expired.");
        }

        if (!TryApplyDecision(gatePass, email, approve, remarks, out var errorMessage))
        {
            return RenderLinkResultPage("Unable to Process", errorMessage ?? "This link could not be processed.");
        }

        await repository.UpdateAsync(id, gatePass, cancellationToken);

        if (approve)
        {
            if (gatePass.Status == "Approved")
            {
                await SendFinalDecisionEmailAsync(gatePass, true, templateRepository, emailService, loggerFactory, cancellationToken);
            }
            else
            {
                await SendStageRequestEmailsAsync(
                    gatePass,
                    gatePass.StatusLevel!.Value,
                    templateRepository,
                    emailService,
                    notificationSettings,
                    loggerFactory,
                    cancellationToken);
            }

            return RenderLinkResultPage("Approved", $"You have approved gate pass {gatePass.VisitorPassReferenceNo}.");
        }

        await SendFinalDecisionEmailAsync(gatePass, false, templateRepository, emailService, loggerFactory, cancellationToken);

        return RenderLinkResultPage("Rejected", $"You have rejected gate pass {gatePass.VisitorPassReferenceNo}.");
    }

    private static IResult RenderLinkResultPage(string heading, string message)
    {
        var html =
            $"""
            <!doctype html>
            <html>
            <head><title>Gate Pass Approval</title></head>
            <body style="font-family: sans-serif; text-align: center; padding: 60px;">
                <h2>{heading}</h2>
                <p>{message}</p>
            </body>
            </html>
            """;

        return Results.Content(html, "text/html");
    }

    private static bool TryApplyDecision(
        GatePassEntity gatePass,
        string approverEmail,
        bool approve,
        string? remarks,
        out string? errorMessage)
    {
        errorMessage = null;

        if (gatePass.Status is "Approved" or "Rejected")
        {
            errorMessage = $"Gate pass has already been {gatePass.Status}.";
            return false;
        }

        if (gatePass.ApproverChain.Count == 0 ||
            gatePass.StatusLevel is null ||
            gatePass.StatusLevel >= gatePass.ApproverChain.Count)
        {
            errorMessage = "There is no pending approval step for this gate pass.";
            return false;
        }

        var level = gatePass.StatusLevel.Value;
        var expectedApprovers = GetExpectedApproversForLevel(gatePass, level);

        if (!expectedApprovers.Any(e => string.Equals(e, approverEmail, StringComparison.OrdinalIgnoreCase)))
        {
            errorMessage = $"{approverEmail} is not the expected approver for this level.";
            return false;
        }

        gatePass.Transactions.Add(new VisitorGatePassTransaction
        {
            Description = $"{level} Level Approval",
            LevelStatus = approve ? "Approved" : "Rejected",
            CreatedBy = approverEmail,
            CreatedOn = DateTime.UtcNow
        });

        if (!approve)
        {
            gatePass.Status = "Rejected";
            gatePass.ApprovedRemarks = remarks;
            gatePass.IsLevelProcessed = true;
            gatePass.ProcessedBy = approverEmail;
            gatePass.ProcessedAt = DateTime.UtcNow;

            return true;
        }

        var isFinalLevel = level == gatePass.MaxApprovalLevel!.Value - 1;

        if (isFinalLevel)
        {
            gatePass.Status = "Approved";
            gatePass.ApprovedBy = approverEmail;
            gatePass.ApprovedOn = DateTime.UtcNow;
            gatePass.ApprovedRemarks = remarks;
            gatePass.IsLevelProcessed = true;
            gatePass.ProcessedBy = approverEmail;
            gatePass.ProcessedAt = DateTime.UtcNow;
        }
        else
        {
            gatePass.StatusLevel = level + 1;
        }

        return true;
    }

    private static List<string> GetExpectedApproversForLevel(GatePassEntity gatePass, int level)
    {
        return level < gatePass.ApproverChain.Count
            ? [gatePass.ApproverChain[level]]
            : [];
    }

    private static List<string> BuildEffectiveApproverChain(string? hostEmail, List<string> precedenceEmails)
    {
        var chain = new List<string>();

        if (!string.IsNullOrWhiteSpace(hostEmail))
        {
            chain.Add(hostEmail);
        }

        foreach (var email in precedenceEmails)
        {
            if (chain.Count == 0 || !string.Equals(chain[^1], email, StringComparison.OrdinalIgnoreCase))
            {
                chain.Add(email);
            }
        }

        return chain;
    }

    private static async Task<IResult> UploadDocumentAsync(
        string id,
        IFormFile file,
        [FromForm] string documentType,
        IVisitorGatePassRepository repository,
        IMediaStorageClient mediaStorageClient,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid gate pass id." });
        }

        if (file.Length == 0)
        {
            return Results.BadRequest(
                new { message = "File is required." });
        }

        var key = NormalizeDocumentTypeKey(documentType);

        if (!DocumentTypeMap.TryGetValue(key, out var mapping))
        {
            return Results.BadRequest(new
            {
                message = "documentType must be one of: Photo, Passport(EID), Visa, Supporting Docs, National ID."
            });
        }

        var gatePass = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (gatePass is null)
        {
            return Results.NotFound();
        }

        string documentUrl;
        try
        {
            await using var stream = file.OpenReadStream();

            documentUrl = await mediaStorageClient.UploadAsync(
                stream,
                file.FileName,
                file.ContentType,
                mapping.Category,
                cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }

        var document = gatePass.VisitorDocuments.FirstOrDefault(d =>
            NormalizeDocumentTypeKey(d.DocType ?? string.Empty) == key);

        if (document is null)
        {
            document = new GatePassDocumentEntity { DocType = mapping.CanonicalType };
            gatePass.VisitorDocuments.Add(document);
        }

        document.Upload = documentUrl;

        var updated = await repository.UpdateAsync(
            id,
            gatePass,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorGatePassResponse.FromEntity(gatePass))
            : Results.NotFound();
    }

    private static string NormalizeDocumentTypeKey(string documentType)
    {
        return new string(documentType.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
    }

    private static string GenerateSixDigitCode()
    {
        return Random.Shared.Next(100000, 1000000).ToString(CultureInfo.InvariantCulture);
    }

    private static string GenerateReferenceNo()
    {
        var suffix = Random.Shared.Next(0, 10000).ToString("D4", CultureInfo.InvariantCulture);

        return $"VPRN{DateTime.UtcNow:yyyyMMdd}-{suffix}";
    }

    private static string ComputeLevelToken(string gatePassId, int level, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var payload = Encoding.UTF8.GetBytes($"{gatePassId}|{level}");

        return Convert.ToHexString(hmac.ComputeHash(payload)).ToLowerInvariant();
    }

    private static string BuildActionLink(string apiBaseUrl, string gatePassId, string action, string email, string token)
    {
        var baseUrl = apiBaseUrl.TrimEnd('/');

        return $"{baseUrl}/api/visitorgatepasses/{gatePassId}/{action}-by-link?email={Uri.EscapeDataString(email)}&token={token}";
    }

    private static string RenderTemplate(string body, Dictionary<string, string> tokens)
    {
        var rendered = body;

        foreach (var (key, value) in tokens)
        {
            rendered = rendered.Replace($"{{{{{key}}}}}", value, StringComparison.OrdinalIgnoreCase);
        }

        return Regex.Replace(rendered, "{{.*?}}", string.Empty);
    }

    private static async Task<(string Subject, string Body)> GetTemplateAsync(
        IEmailTemplateRepository templateRepository,
        string templateName,
        string defaultSubject,
        string defaultBody,
        CancellationToken cancellationToken)
    {
        var template = await templateRepository.GetByNameAsync(templateName, cancellationToken);

        return template is null
            ? (defaultSubject, defaultBody)
            : (template.Subject ?? defaultSubject, template.Body ?? defaultBody);
    }

    private static async Task SendStageRequestEmailsAsync(
        GatePassEntity gatePass,
        int level,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        GatePassNotificationSettings notificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var recipients = GetExpectedApproversForLevel(gatePass, level);

        if (recipients.Count == 0)
        {
            return;
        }

        var (subject, bodyTemplate) = await GetTemplateAsync(
            templateRepository,
            StageRequestTemplateName,
            $"Visitor Gate Pass Approval Required - {gatePass.VisitorPassReferenceNo}",
            DefaultStageRequestTemplateBody,
            cancellationToken);

        var token = ComputeLevelToken(gatePass.Id!, level, notificationSettings.LinkSecret);

        foreach (var recipient in recipients)
        {
            try
            {
                var displayName =
                    string.Equals(recipient, gatePass.HostPersonEmail, StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(gatePass.HostPerson)
                        ? gatePass.HostPerson
                        : recipient;

                var tokens = new Dictionary<string, string>
                {
                    ["referenceno"] = gatePass.VisitorPassReferenceNo ?? string.Empty,
                    ["fullname"] = displayName,
                    ["contactname"] = gatePass.ContactName ?? string.Empty,
                    ["hostperson"] = gatePass.HostPerson ?? string.Empty,
                    ["hostcompany"] = gatePass.HostCompany ?? string.Empty,
                    ["visitorcompany"] = gatePass.VisitorCompany ?? string.Empty,
                    ["reasonofvisit"] = gatePass.ReasonOfVisit ?? string.Empty,
                    ["fromdate"] = gatePass.FromDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ["todate"] = gatePass.ToDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ["approveurl"] = BuildActionLink(notificationSettings.ApiBaseUrl, gatePass.Id!, "approve", recipient, token),
                    ["rejecturl"] = BuildActionLink(notificationSettings.ApiBaseUrl, gatePass.Id!, "reject", recipient, token)
                };

                await emailService.SendEmailAsync(
                    recipient,
                    RenderTemplate(subject, tokens),
                    RenderTemplate(bodyTemplate, tokens),
                    cancellationToken,
                    isHtml: true);
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger("VisitorGatePassEndpoints")
                    .LogError(ex, "Failed to send gate pass approval request email to {Email}", recipient);
            }
        }
    }

    private static async Task SendFinalDecisionEmailAsync(
        GatePassEntity gatePass,
        bool approved,
        IEmailTemplateRepository templateRepository,
        IEmailService emailService,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var recipients = new List<string>();

        if (!string.IsNullOrWhiteSpace(gatePass.EmailId))
        {
            recipients.Add(gatePass.EmailId);
        }

        if (!string.IsNullOrWhiteSpace(gatePass.HostPersonEmail) &&
            !recipients.Any(r => string.Equals(r, gatePass.HostPersonEmail, StringComparison.OrdinalIgnoreCase)))
        {
            recipients.Add(gatePass.HostPersonEmail);
        }

        if (recipients.Count == 0)
        {
            return;
        }

        var templateName = approved ? ApprovedTemplateName : RejectedTemplateName;
        var defaultSubject = approved
            ? "Purple IQ - Gate Pass Approval "
            : $"Purple IQ - Gate Pass Rejected - {gatePass.VisitorPassReferenceNo}";
        var defaultBody = approved ? DefaultApprovedTemplateBody : DefaultRejectedTemplateBody;

        var (subject, bodyTemplate) = await GetTemplateAsync(
            templateRepository,
            templateName,
            defaultSubject,
            defaultBody,
            cancellationToken);

        var processedBy = string.IsNullOrWhiteSpace(gatePass.ProcessedBy) ? "Purple IQ Team" : gatePass.ProcessedBy;

        var tokens = new Dictionary<string, string>
        {
            ["referenceno"] = gatePass.VisitorPassReferenceNo ?? string.Empty,
            ["fullname"] = processedBy,
            ["remarks"] = gatePass.ApprovedRemarks ?? string.Empty,
            ["suppoertmobileno"] = string.Empty,
            ["support_url"] = string.Empty,
            ["supportemail"] = string.Empty
        };

        var renderedSubject = RenderTemplate(subject, tokens);
        var renderedBody = RenderTemplate(bodyTemplate, tokens);

        foreach (var recipient in recipients)
        {
            try
            {
                await emailService.SendEmailAsync(
                    recipient,
                    renderedSubject,
                    renderedBody,
                    cancellationToken,
                    isHtml: true);
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger("VisitorGatePassEndpoints")
                    .LogError(
                        ex,
                        "Failed to send gate pass {Decision} email to {Email}",
                        approved ? "approved" : "rejected",
                        recipient);
            }
        }
    }
}
