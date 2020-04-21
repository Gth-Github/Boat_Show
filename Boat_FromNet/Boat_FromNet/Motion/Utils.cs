using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boat_FromNet.Motion    //Boat_FromNet.Motion
{
    public class Utils
    {
        //根据x方向的位移和y方向的位移计算，直线距离和角度
        public static void GetDistanceAndCourseFromOffset(double xOffset, double yOffset, ref double distance, ref double course)
        {
            distance = Math.Sqrt(xOffset * xOffset + yOffset * yOffset);
            if (xOffset > 0 && yOffset > 0)
            {
                course = Math.Atan2(xOffset, yOffset);
            }
            else if (xOffset > 0 && yOffset == 0)
            {
                course = Common.PI / 2;
            }
            else if (xOffset > 0 && yOffset < 0)
            {
                course = Common.PI / 2 + Math.Atan2(-yOffset, xOffset);
            }
            else if (xOffset == 0 && yOffset > 0)
            {
                course = 0.0;
            }
            else if (xOffset == 0 && yOffset == 0)
            {
                course = 0.0;
            }
            else if (xOffset == 0 && yOffset < 0)
            {
                course = Common.PI;
            }
            else if (xOffset < 0 && yOffset > 0)
            {
                course = Common.PI / 2 * 3 + Math.Atan2(yOffset, -xOffset);
            }
            else if (xOffset < 0 && yOffset == 0)
            {
                course = Common.PI / 2 * 3;
            }
            else if (xOffset < 0 && yOffset < 0)
            {
                course = Common.PI + Math.Atan2(-xOffset, -yOffset);
            }
            course = course / Common.PI * 180;
        }
    };
}
