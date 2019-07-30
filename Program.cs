using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Program
    { 
        static void Main(string[] args)
        {
            Platform platform = new Platform();
            Robot robot = new Robot();

            Console.WriteLine("Payload with Robot Landing at (" + robot.currentPosition.X + ", " + robot.currentPosition.Y + ")... landed");
            robot.assignPlatform(platform);

            string input;
            Console.WriteLine("Move forward(f), move backward(b), turn left(l), turn right(r) OR exit:");
            input = Console.ReadLine();

            while(!input.Equals("exit"))
            {
                bool lastMoveSuccess = true;

                foreach (char ch in input)
                {
                    //break out of loop if encountered obstacle
                    if(lastMoveSuccess == false)
                        break;

                    //switch ignore other non valid commands
                    switch(ch)
                    {
                        case 'f':   lastMoveSuccess = robot.moveForward();
                            break;

                        case 'b':    lastMoveSuccess = robot.moveBackward();
                            break;

                        case 'l':   robot.turnLeft();
                            break;

                        case 'r':   robot.turnRight();
                            break;
                    }
                }

                robot.displayNewPosition();

                Console.WriteLine("Move forward(f), move backward(b), turn left(l), turn right(r) OR exit:");
                input = Console.ReadLine();
            }
        }
    }
}