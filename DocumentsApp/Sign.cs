using System;

namespace DocumentsApp
{
    public class Sign
    {
        public Guid Id { get; set; }
        public virtual Person Signer { get; set; }
        public Document SignedDocument { get; set; }

        public Sign()
        {
            Id = Guid.NewGuid();
        }
    }
}
