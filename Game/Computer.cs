using System;
using System.Text;
using System.Security.Cryptography;

namespace Game
{
    class Computer
    {       
        public int Move { get; set; }
        public byte[] Hmac { get; set; }
        public byte[] SecretKey { get; set; }

        private string[] Moves;

        public Computer(string[] moves)
        {
            Moves = moves;
        }
        
        public void MakeMove()
        {
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                SecretKey = new byte[16];
                rngCsp.GetBytes(SecretKey);
            }

            byte roll = ComputerStroke((byte)Moves.Length);
            Move = roll - 1;

            using (HMACSHA256 hmac = new HMACSHA256(SecretKey))
            {
                Hmac = hmac.ComputeHash(Encoding.ASCII.GetBytes(Moves[Move]));
            }
        }

        public byte ComputerStroke(byte numberMoves)
        {
            if (numberMoves <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            byte[] randomNumber = new byte[1];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                do
                {
                    rngCsp.GetBytes(randomNumber);
                }
                while (!IsFairRoll(randomNumber[0], numberMoves));
            }               
            
            return (byte)((randomNumber[0] % numberMoves) + 1);
        }

        private bool IsFairRoll(byte roll, byte numSides)
        {
            int fullSetsOfValues = Byte.MaxValue / numSides;
            
            return roll < numSides * fullSetsOfValues;
        }
    }
}
