// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("e2i7Txs0Ni1BkiW2Lbj6dVqgZSU0ZI8hDiS4PoPeHtCdP275g+mvFm3u4O/fbe7l7W3u7u9YmuB90VNcw4RpYALkwlaqzvdeyf4M5J4gBR/fbe7N3+Lp5sVpp2kY4u7u7urv7Cf4984jttR2syp3NK582ZYwN+glgp+hSrEXFt+KMFa7oDcpdWQs50NBy0+TYw2+6pdM8evWgtl19epZJvMhet1nxZ0g2cWksy5EnsSvHW1ftvJ2qOnSQG8C4YSoHTGXAi6fyuHQ63qbJH2K84Iq6N4iPYXuGxPtyLM2mkLZ3oOpnym9CMK8s7nzcjfooC/IWadr21w2v81Q+LBWJJtK5qII7Asa8q27YF0jh8TOofSfNFY2981beRk42XoT5u3s7u/u");
        private static int[] order = new int[] { 6,5,5,11,6,11,7,7,12,10,13,13,12,13,14 };
        private static int key = 239;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
