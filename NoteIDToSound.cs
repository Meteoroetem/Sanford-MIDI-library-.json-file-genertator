using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanford_MIDI
{
    internal class NoteIDToSound
    {
        public static int Convert(int noteID)
        {
            switch (noteID)
            {
                default:
                    return 0;
                case 48:
                    return 131;
                case 49:
                    return 139;
                case 50:
                    return 147;
                case 51:
                    return 156;
                case 52:
                    return 165;
                case 53:
                    return 175;
                case 54:
                    return 185;
                case 55:
                    return 196;
                case 56:
                    return 208;
                case 57:
                    return 220;
                case 58:
                    return 233;
                case 59:
                    return 247;
                case 60:
                    return 262;
                case 61:
                    return 277;
                case 62:
                    return 294;
                case 63:
                    return 311;
                case 64:
                    return 330;
                case 65:
                    return 349;
                case 66:
                    return 370;
                case 67:
                    return 392;
                case 68:
                    return 415;
                case 69:
                    return 440;
                case 70:
                    return 466;
                case 71:
                    return 494;
                case 72:
                    return 523;
                case 73:
                    return 554;
                case 74:
                    return 587;
                case 75:
                    return 622;
                case 76:
                    return 659;
                case 77:
                    return 598;
                case 78:
                    return 740;
                case 79:
                    return 784;
                case 80:
                    return 831;
                case 81:
                    return 880;
                case 82:
                    return 932;
                case 83:
                    return 988;
                case 84:
                    return 1047;
            }
        }
    }
}
