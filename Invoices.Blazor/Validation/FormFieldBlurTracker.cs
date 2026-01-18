namespace Invoices.Blazor.Validation
{
    /// <summary>
    /// Tracks blur and focus state for individual form fields, allowing validators
    /// to determine whether a field has already been blurred by the user.
    /// </summary>
    public class FormFieldBlurTracker
    {
        private readonly HashSet<string> _blurred = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the specified field has been blurred at least once.
        /// </summary>
        /// <param name="key">Unique identifier of the form field.</param>
        /// <returns>
        /// <c>true</c> if the field has been marked as blurred; otherwise <c>false</c>.
        /// </returns>
        public bool IsBlurred(string key) => _blurred.Contains(key);

        /// <summary>
        /// Marks the specified field as blurred, indicating that the user has left the field.
        /// </summary>
        /// <param name="key">Unique identifier of the form field.</param>
        public void MarkBlurred(string key) => _blurred.Add(key);

        /// <summary>
        /// Marks the specified field as focused, removing any previous blur state.
        /// </summary>
        /// <param name="key">Unique identifier of the form field.</param>
        public void MarkFocused(string key) => _blurred.Remove(key);

        /// <summary>
        /// Resets blur tracking for the entire form or for all fields whose keys
        /// begin with the specified prefix.
        /// </summary>
        /// <param name="prefix">
        /// Optional prefix used to limit which fields are reset.
        /// When omitted or empty, all tracked fields are cleared.
        /// </param>
        public void ResetForm(string prefix = "")
        {
            if (string.IsNullOrEmpty(prefix))
                _blurred.Clear();
            else
                _blurred.RemoveWhere(k => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }
    }

}
