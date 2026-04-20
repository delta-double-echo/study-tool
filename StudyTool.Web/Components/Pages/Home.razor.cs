using Microsoft.AspNetCore.Components;
using StudyTool.Core.Interfaces;
using StudyTool.Core.Models;

namespace StudyTool.Web.Components.Pages;

public partial class Home
{
    [Inject] private ICardService CardService { get; set; } = null!;
    [Inject] private IGroupService GroupService { get; set; } = null!;

    private IEnumerable<Card> allCards = [];
    private IEnumerable<Card> filteredCards = [];
    private IEnumerable<Group> groups = [];
    private Guid? selectedGroupId = null;
    private bool showFlaggedOnly = false;
    private string searchTerm = string.Empty;
    private bool isLoading = true;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        isLoading = true;
        errorMessage = null;
        try
        {
            allCards = await CardService.GetAllAsync();
            groups = await GroupService.GetAllAsync("owner");
            ApplyFilters();
        }
        catch (Exception)
        {
            errorMessage = "Could not connect to the database. Please refresh the page.";
        }
        finally
        {
            isLoading = false;
        }
    }

    private void FilterCards()
    {
        ApplyFilters();
        StateHasChanged();
    }

    private void SelectGroup(Guid? groupId)
    {
        selectedGroupId = groupId;
        ApplyFilters();
        StateHasChanged();
    }

    private void ToggleFlagged()
    {
        showFlaggedOnly = !showFlaggedOnly;
        ApplyFilters();
        StateHasChanged();
    }

    private void ApplyFilters()
    {
        var result = allCards.AsQueryable();

        if (selectedGroupId.HasValue)
            result = result.Where(c => c.GroupId == selectedGroupId.Value);

        if (showFlaggedOnly)
            result = result.Where(c => c.FlaggedForImprovement);

        if (!string.IsNullOrWhiteSpace(searchTerm))
            result = result.Where(c =>
                c.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

        filteredCards = result.ToList();
    }

    private void OnSearchInput(ChangeEventArgs e)
    {
        searchTerm = e.Value?.ToString() ?? string.Empty;
        ApplyFilters();
        StateHasChanged();
    }

    private string GroupItemClass(Guid? groupId) =>
        selectedGroupId == groupId
            ? "bg-orange-50 text-orange-800 font-medium"
            : "text-gray-600 hover:bg-gray-100";

    private string FlagFilterClass() =>
        showFlaggedOnly
            ? "bg-orange-50 text-orange-800 font-medium"
            : "text-gray-600 hover:bg-gray-100";
}