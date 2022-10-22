using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OrangeRedDotNet.BlazorWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Text edit with suggestions
    /// </summary>
    /// <typeparam name="TItem">Suggestion object</typeparam>
    public partial class TextSuggest<TItem>
    {
        /// <summary>
        /// Theme service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }
        /// <summary>
        /// JS Interop
        /// </summary>
        [Inject]
        public IJSRuntime JSInterop { get; set; }

        /// <summary>
        /// Text edit value
        /// </summary>
        [Parameter]
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                {
                    return;
                }
                _text = value;
                TextChanged.InvokeAsync(value);
            }
        }
        /// <summary>
        /// Text changed event callback
        /// </summary>
        [Parameter]
        public EventCallback<string> TextChanged { get; set; }
        /// <summary>
        /// If the text edit is disabled or not
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; } = false;
        /// <summary>
        /// Placeholder for the text edit
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; }
        /// <summary>
        /// Function to get suggestions based on a text value
        /// </summary>
        [Parameter]
        public Func<string, Task<IEnumerable<TItem>>> GetSuggestions { get; set; }
        /// <summary>
        /// Function to get text value from a suggestion
        /// </summary>
        [Parameter]
        public Func<TItem, string> GetTextValue { get; set; }
        /// <summary>
        /// Render fragment for a suggestion
        /// </summary>
        [Parameter]
        public RenderFragment<TItem> SuggestionContent { get; set; }
        /// <summary>
        /// Event callback for when the text is no longer in focus
        /// </summary>
        [Parameter]
        public EventCallback OnTextBlur { get; set; }
        /// <summary>
        /// Event callback for when the text gets focus
        /// </summary>
        [Parameter]
        public EventCallback OnTextFocus { get; set; }
        /// <summary>
        /// Event callback for when a suggestion is selected
        /// </summary>
        [Parameter]
        public EventCallback OnSuggestionSelected { get; set; }

        /// <summary>
        /// Guid for this component
        /// </summary>
        private readonly Guid _elementGuid = new();

        /// <summary>
        /// Internal text value
        /// </summary>
        private string _text;
        /// <summary>
        /// If the dropdown can be shown or not
        /// </summary>
        private bool _canShowDropdown = false;
        /// <summary>
        /// Index of highlighted suggestion
        /// </summary>
        private int _activeSuggestionIndex = -1;
        /// <summary>
        /// Current set of suggestions
        /// </summary>
        private IEnumerable<TItem> _currentSuggestions;

        /// <summary>
        /// Number of suggestions available
        /// </summary>
        private int NumberOfSuggestions => _currentSuggestions?.Count() ?? 0;
        /// <summary>
        /// If the dropdown is visible or not
        /// </summary>
        private bool DropdownVisible => _canShowDropdown && (_currentSuggestions != null);

        /// <summary>
        /// Handle when the text is changed
        /// </summary>
        /// <param name="text">New text value</param>
        /// <returns>Awaitable task</returns>
        protected async Task HandleTextChanged(string text)
        {
            Text = text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                _currentSuggestions = await GetSuggestions(text);
                Open();
                _activeSuggestionIndex = 0;
            }
            else
            {
                _currentSuggestions = null;
                Close();
            }
        }

        /// <summary>
        /// Handle for when the text edit has lost focus
        /// </summary>
        /// <param name="eventArgs">Event args</param>
        protected async Task HandleBlur(FocusEventArgs eventArgs)
        {
            Close();
            if (OnTextBlur.HasDelegate)
            {
                await OnTextBlur.InvokeAsync();
            }
        }

        /// <summary>
        /// Handle for when text edit gets focus
        /// </summary>
        /// <param name="eventArgs">Event args</param>
        /// <returns>Awaitable task</returns>
        protected async Task HandleFocus(FocusEventArgs eventArgs)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                _currentSuggestions ??= await GetSuggestions(Text);
                Open();
            }
            else
            {
                Close();
            }
            if (OnTextFocus.HasDelegate)
            {
                await OnTextFocus.InvokeAsync();
            }
        }

        /// <summary>
        /// Handle for the keydown event on the text edit
        /// </summary>
        /// <param name="eventArgs">Event args</param>
        /// <returns>Awaitable task</returns>
        protected async Task HandleKeyDown(KeyboardEventArgs eventArgs)
        {
            switch (eventArgs.Code)
            {
                case "Escape":
                    Close();
                    return;
                case "ArrowUp":
                    UpdateActiveSuggestionIndex(_activeSuggestionIndex - 1);
                    break;
                case "ArrowDown":
                    UpdateActiveSuggestionIndex(_activeSuggestionIndex + 1);
                    break;
                case "Enter":
                case "NumpadEnter":
                    var currentActiveSuggestion = _currentSuggestions.ElementAt(_activeSuggestionIndex);
                    await HandleItemSelected(currentActiveSuggestion);
                    return;
                default:
                    break;
            }
            Open();
            await JSInterop.InvokeVoidAsync("scrollElementIntoView", 
                GetDropdownItemId(Math.Max(0, _activeSuggestionIndex)));
        }

        /// <summary>
        /// Handle for when an item is selected
        /// </summary>
        /// <param name="selectedItem">Selected item</param>
        protected async Task HandleItemSelected(TItem selectedItem)
        {
            Text = GetTextValue(selectedItem);
            _currentSuggestions = new List<TItem> { selectedItem };
            Close();
            if (OnSuggestionSelected.HasDelegate)
            {
                await OnSuggestionSelected.InvokeAsync();
            }
        }

        /// <summary>
        /// Get an ID for a dropdown item
        /// </summary>
        /// <param name="index">Index of the dropdown item</param>
        /// <returns>ID string</returns>
        protected string GetDropdownItemId(int index)
        {
            return $"text-suggest-{_elementGuid}-{index}";
        }

        /// <summary>
        /// Get the class name(s) for a dropdown item
        /// </summary>
        /// <param name="index">Index of the dropdown item</param>
        /// <returns>String of class name(s)</returns>
        protected string GetDropdownItemClass(int index)
        {
            if (index == _activeSuggestionIndex)
            {
                return $"suggest-item-active{(ThemeService.AppTheme.DarkMode ? "-dark" : "")}";
            }
            return string.Empty;
        }

        /// <summary>
        /// Open the dropdown
        /// </summary>
        private void Open()
        {
            if (!_canShowDropdown)
            {
                _activeSuggestionIndex = 0;
                _canShowDropdown = true;
            }
        }

        /// <summary>
        /// Close the dropdown
        /// </summary>
        private void Close()
        {
            if (_canShowDropdown)
            {
                _activeSuggestionIndex = -1;
                _canShowDropdown = false;
            }
        }

        /// <summary>
        /// Update the active suggestion index
        /// </summary>
        /// <param name="newIndex">New active index</param>
        private void UpdateActiveSuggestionIndex(int newIndex)
        {
            if (newIndex < 0)
            {
                _activeSuggestionIndex = 0;
            }
            else if ((newIndex + 1) > NumberOfSuggestions)
            {
                _activeSuggestionIndex = NumberOfSuggestions - 1;
            }
            else
            {
                _activeSuggestionIndex = newIndex;
            }
        }
    }
}
