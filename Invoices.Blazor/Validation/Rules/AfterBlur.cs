namespace Invoices.Blazor.Validation.Rules;

public partial class RulesValidator
{
    // TODO: Update XML comment
    /// <summary>
    /// Wraps an existing validator so that it only executes
    /// after the specified field has been blurred.
    /// </summary>
    /// <param name="field">
    /// The field identifier tracked by <see cref="FormFieldBlurTracker"/>.
    /// </param>
    /// <param name="inner">
    /// The underlying validation function to execute after blur.
    /// Returns <c>null</c> when valid or an error message when invalid.
    /// </param>
    /// <param name="tracker">
    /// The blur tracker used to determine whether the field
    /// has been blurred at least once.
    /// </param>
    /// <returns>
    /// A validator that returns <c>null</c> until the field is blurred.
    /// After blur, it delegates to the wrapped validator and returns its result.
    /// </returns>
    public Func<object?, string?> AfterBlur(
        string field,
        Func<object?, string?> inner,
        FormFieldBlurTracker tracker)
    {
        return value =>
        {
            // Neopuštěno = nevaliduje
            if (!tracker.IsBlurred(field))
                return null;

            // Opuštěno = validuje
            return inner(value);
        };
    }
}

