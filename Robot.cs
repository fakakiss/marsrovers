using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    //enumerable type to specify direction
    enum Direction { N, S, E, W};

    class Robot
    {
        //Robot Attributes
        public Point currentPosition { get; private set; }
        public Direction currentDirection { get; private set; }

        private Platform platform;     //current location on platform

        //constructor that takes initial position and direction
        public Robot(int startingX = 0, int startingY = 0, Direction startingDir = Direction.N)
        {
            currentPosition = new Point(startingX, startingY);
            currentDirection = startingDir;
        }

        //method to assign platform to rover
        public void assignPlatform(Platform platform)
        {
            this.platform = platform;

            if (platform.obstacles.Contains(currentPosition))
            {
                Console.Write("Grid has obstacle at rover starting position. Landed rover at ");
                //verify that current position of robot is not an obstacle
                while (platform.obstacles.Contains(currentPosition))
                    moveForward();

                Console.Write("(" + currentPosition.X + ", " + currentPosition.Y + ") instead\n");
            }
        }

        //method to move rover one step forward
        //displays error (and return false) if obstalce lies ahead
        public bool moveForward()
        {
            switch(currentDirection)
            {
                case Direction.N: incrementY();
                    break;

                case Direction.S: decrementY();
                    break;

                case Direction.E: incrementX();
                    break;

                case Direction.W: decrementX();
                    break;
            }

            //if new current position has obstacle, move back and report it
            if(platform.obstacles.Contains(currentPosition))
            {
                Console.WriteLine("Cannot move forward. Obstacle present ahead at (" + currentPosition.X + ", " + currentPosition.Y + ")");
                moveBackward();
                return false;
            }

            return true;
        }

        //method to move rover one step backward
        //displays error (and return false) if obstalce lies behind
        public bool moveBackward()
        {
            switch (currentDirection)
            {
                case Direction.N: decrementY();
                    break;

                case Direction.S: incrementY();
                    break;

                case Direction.E: decrementX();
                    break;

                case Direction.W: incrementX();
                    break;
            }

            //if new current position has obstacle, move back and report it
            if (platform.obstacles.Contains(currentPosition))
            {
                Console.WriteLine("Cannot move backward. Obstacle present behind at (" + currentPosition.X + ", " + currentPosition.Y + ")");
                moveForward();
                return false;
            }

            return true;
        }

        //method to turn rover to the left
        public void turnLeft()
        {
            switch (currentDirection)
            {
                case Direction.N: currentDirection = Direction.W;
                    break;

                case Direction.S: currentDirection = Direction.E;
                    break;

                case Direction.E: currentDirection = Direction.N;
                    break;

                case Direction.W: currentDirection = Direction.S;
                    break;
            }
        }

        //method to turn rover to the right
        public void turnRight()
        {
            switch (currentDirection)
            {
                case Direction.N: currentDirection = Direction.E;
                    break;

                case Direction.S: currentDirection = Direction.W;
                    break;

                case Direction.E: currentDirection = Direction.S;
                    break;

                case Direction.W: currentDirection = Direction.N;
                    break;
            }
        }

        //method to increment rover's x position (with wrap around)
        private void incrementX()
        {
            int newX = currentPosition.X + 1;

            if (newX > platform.maxX)
                newX = platform.minX;

            currentPosition = new Point(newX, currentPosition.Y);
        }

        //method to decrement rover's x position (with wrap around)
        private void decrementX()
        {
            int newX = currentPosition.X - 1;

            if (newX < platform.minX)
                newX = platform.maxX;

            currentPosition = new Point(newX, currentPosition.Y);
        }

        //method to increment rover's y position (with wrap around)
        private void incrementY()
        {
            int newY = currentPosition.Y + 1;

            if (newY > platform.maxY)
                newY = platform.minY;

            currentPosition = new Point(currentPosition.X, newY);
        }

        //method to decrement rover's y position (with wrap around)
        private void decrementY()
        {
            int newY = currentPosition.Y - 1;

            if (newY < platform.minY)
                newY = platform.maxY;

            currentPosition = new Point(currentPosition.X, newY);
        }

        //method to display new rover position and direction
        public void displayNewPosition()
        {
            Console.Write("New rover position is (" + currentPosition.X + ", " + currentPosition.Y + ") facing ");

            switch(currentDirection)
            {
                case Direction.N: Console.Write("North.\n");
                    break;

                case Direction.S: Console.Write("South.\n");
                    break;

                case Direction.E: Console.Write("East.\n");
                    break;

                case Direction.W: Console.Write("West.\n");
                    break;
            }
        }
    }
}