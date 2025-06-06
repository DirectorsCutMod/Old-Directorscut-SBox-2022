﻿using Sandbox;
using Tools;

// - SFMDX -
// Source Filmmaker in S&box
// Licensed under the MIT License
// 
// Copyright (c) 2022 KiwifruitDev
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace SFMDX;

public class AnimationSetEditor : Widget
{
	Color color;

	public AnimationSetEditor(Widget parent = null) : base(parent)
	{
		// Layout top to bottom
		SetLayout( LayoutMode.TopToBottom );

		var button = new Button( "Change Color", "color_lens" );
		button.Clicked = () =>
		{
			color = Color.Random;
			Update();
		};

		// Fill the top
		Layout.AddStretchCell();

		// Add a new layout cell to the bottom
		var bottomRow = Layout.Add( LayoutMode.LeftToRight );
		bottomRow.Margin = 16;
		bottomRow.AddStretchCell();
		bottomRow.Add( button );
	}

	protected override void OnPaint()
	{
		base.OnPaint();

		Paint.ClearPen();
		Paint.SetBrush( color );
		Paint.DrawRect( LocalRect );
	}
}
