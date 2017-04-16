using MadWare.Passbook.PassSerializer;
using MadWare.Passbook.PassSigner;
using MadWare.Passbook.PassStyle;

namespace MadWare.Passbook
{
    public class PassGenerator
    {
        protected PassGeneratorOptions passOptions;

        protected IPassSerializer passSerializer;

        protected IPassSigner passSigner;

        public PassGenerator(PassGeneratorOptions opt, IPassSerializer passSerializer, IPassSigner passSigner)
        {
            this.passOptions = opt;
            this.passSerializer = passSerializer;
            this.passSigner = passSigner;
        }

        public byte[] Generate<T>(Pass<T> p) where T : BasePassStyle
        {
            return null;
        }
    }
}