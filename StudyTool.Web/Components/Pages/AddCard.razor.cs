using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StudyTool.Core.Interfaces;
using StudyTool.Core.Models;

namespace StudyTool.Web.Components.Pages;

public partial class AddCard
{
    [Inject] private ICardService CardService { get; set; } = null!;
    [Inject] private IGroupService GroupService { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    private IEnumerable<Group> groups = [];
    private string selectedGroupId = string.Empty;
    private string newGroupName = string.Empty;
    private bool showNewGroup = false;
    private string title = string.Empty;
    private string content = string.Empty;
    private bool flaggedForImprovement = false;
    private string? imagePreview;
    private IBrowserFile? selectedImage;
    private bool isSaving = false;
    private bool saveSuccess = false;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        groups = await GroupService.GetAllAsync("owner");
    }

    private void ToggleNewGroup()
    {
        showNewGroup = !showNewGroup;
        if (!showNewGroup) newGroupName = string.Empty;
    }

    private async Task OnImageSelected(InputFileChangeEventArgs e)
    {
        selectedImage = e.File;
        var buffer = new byte[selectedImage.Size];
        await selectedImage.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024).ReadAsync(buffer);
        var base64 = Convert.ToBase64String(buffer);
        imagePreview = $"data:{selectedImage.ContentType};base64,{base64}";
    }

    private async Task SaveCard()
    {
        errorMessage = null;
        saveSuccess = false;

        if (!Validate()) return;

        isSaving = true;

        try
        {
            var groupId = await ResolveGroupAsync();
            if (groupId == Guid.Empty) return;

            var imagePath = await SaveImageAsync();

            var card = new Card
            {
                GroupId = groupId,
                Title = title.Trim(),
                Content = content.Trim(),
                FlaggedForImprovement = flaggedForImprovement,
                ImagePath = imagePath
            };

            await CardService.CreateAsync(card, "owner");

            saveSuccess = true;
            ResetForm();
            groups = await GroupService.GetAllAsync("owner");
        }
        catch (Exception)
        {
            errorMessage = "Something went wrong saving the card. Please try again.";
        }
        finally
        {
            isSaving = false;
        }
    }

    private bool Validate()
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            errorMessage = "Please enter a question.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(content))
        {
            errorMessage = "Please enter an answer.";
            return false;
        }
        if (!showNewGroup && string.IsNullOrWhiteSpace(selectedGroupId))
        {
            errorMessage = "Please select or create a group.";
            return false;
        }
        if (showNewGroup && string.IsNullOrWhiteSpace(newGroupName))
        {
            errorMessage = "Please enter a name for the new group.";
            return false;
        }
        return true;
    }

    private async Task<Guid> ResolveGroupAsync()
    {
        if (showNewGroup)
        {
            var group = await GroupService.CreateAsync(new Group
            {
                Name = newGroupName.Trim()
            }, "owner");
            return group.Id;
        }

        return Guid.Parse(selectedGroupId);
    }

    private async Task<string?> SaveImageAsync()
    {
        if (selectedImage is null) return null;

        var uploadsPath = Path.Combine("wwwroot", "uploads");
        Directory.CreateDirectory(uploadsPath);

        var extension = Path.GetExtension(selectedImage.Name);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsPath, fileName);

        await using var fs = File.OpenWrite(filePath);
        await selectedImage.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024).CopyToAsync(fs);

        return $"/uploads/{fileName}";
    }

    private void ResetForm()
    {
        title = string.Empty;
        content = string.Empty;
        selectedGroupId = string.Empty;
        newGroupName = string.Empty;
        showNewGroup = false;
        flaggedForImprovement = false;
        imagePreview = null;
        selectedImage = null;
    }
}