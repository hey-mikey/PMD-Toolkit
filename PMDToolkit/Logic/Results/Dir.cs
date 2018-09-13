﻿/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using PMDToolkit.Graphics;
using PMDToolkit.Logic.Display;
using PMDToolkit.Maps;

namespace PMDToolkit.Logic.Results
{
    public class Dir : IResult
    {
        //public ResultType Type { get { return ResultType.Dir; } }
        public RenderTime Delay { get { return RenderTime.Zero; } }

        private int charIndex;
        private Direction8 newDir;

        public int CharIndex { get { return charIndex; } }

        public Dir(int charIndex, Direction8 newDir)
        {
            this.charIndex = charIndex;
            this.newDir = newDir;
        }

        public void Execute()
        {
            if (charIndex < 0)
            {
                Screen.Players[charIndex + Gameplay.Processor.MAX_TEAM_SLOTS].CharDir = newDir;
            }
            else
            {
                Screen.Npcs[charIndex].CharDir = newDir;
            }
        }
    }
}