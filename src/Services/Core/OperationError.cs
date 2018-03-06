namespace IdentityAdmin.Core
{
    /// <summary>
    /// Encapsulates an error.
    /// </summary>
    public class OperationError
    {
        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        public string Description { get; private set; }

        public OperationError(string description, string code = null)
        {
            this.Description = description;
            this.Code = code;
        }

        public override string ToString()
        {
            return this.Code != null ? $"{this.Code} - {this.Description}" : this.Description;
        }
    }
}
