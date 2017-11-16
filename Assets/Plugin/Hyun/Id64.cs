
namespace Hyun
{
    public class Id64
    {
        public Id64()
        {
            this.value = 0;
        }
        public Id64(long value)
        {
            this.value = value;
        }
        public Id64(int left, int right)
        {
            this.value = MakeLong(left, right);
        }
        public long value
        {
            get; private set;
        }
        public int LeftValue()
        {
            return (int)(value >> 32);
        }
        public int RightValue()
        {
            return (int)(value & 0xffffffffL);
        }
        public static int LeftValue(long original)
        {
            return (int)(original >> 32);
        }
        public static int RightValue(long original)
        {
            return (int)(original & 0xffffffffL);
        }

        // left = id, right = uniqueId
        public static long MakeLong(int left, int right)
        {
            //implicit conversion of left to a long
            long res = left;
            //shift the bits creating an empty space on the right
            // ex: 0x0000CFFF becomes 0xCFFF0000
            res = (res << 32);
            //combine the bits on the right with the previous value
            // ex: 0xCFFF0000 | 0x0000ABCD becomes 0xCFFFABCD
            res = res | (long)(uint)right; //uint first to prevent loss of signed bit

            //return the combined result
            return res;
        }
    }
}