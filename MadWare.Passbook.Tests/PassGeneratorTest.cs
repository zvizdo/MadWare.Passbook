using MadWare.Passbook.PassSerializer;

namespace MadWare.Passbook.Tests
{
    class PassGeneratorTest
    {
        public class PassGeneratorMock : PassGenerator
        {
            public PassGeneratorMock(PassGeneratorOptions opt, IPassSerializer passSerializer, NetFrameworkPassSigner passSigner) : base(opt, passSerializer, passSigner)
            {
            }

        }
    }
}
