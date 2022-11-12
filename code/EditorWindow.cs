using System;
using System.Collections.Generic;
using System.Reflection;
using Tools;
using static Tools.Utility;

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

[Tool( "SFMDX", "movie_filter", "Source Filmmaker: Director's Cut")]
public class EditorWindow : Window
{
    public List<DockWidget> DockWidgets;
    private Menu windowsMenu;
	public EditorWindow()
	{
        // Initialize window
		Title = "Source Filmmaker: Director's Cut";
		Size = new Vector2( 1600, 900 );
        Position = new Vector2( 0, 0 );
        DockWidgets = new();
        // Initialize dock widgets
        DockWidget animationSetEditor = ConstructDockWidget<AnimationSetEditor>("Animation Set Editor", "accessibility_new", DockArea.Left);
        ConstructDockWidget<ElementViewer>("Element Viewer", "article", DockArea.Left, animationSetEditor);
        DockWidget timeline = ConstructDockWidget<Timeline>("Timeline", "theaters", DockArea.Bottom);
        ConstructDockWidget<Viewport>("Viewport", "videocam", DockArea.Top);
        ConstructDockWidget<AssetBrowser>("Asset Browser", "ManageSearch", DockArea.Bottom, timeline);
        BuildMenu(); // Build menu bar
		Show(); // Show window
	}

    // use "this" as parent when constructing widgets
    public DockWidget ConstructDockWidget<T>(string title = "", string icon = null, DockArea dockArea = DockArea.Right, DockWidget parent = null) where T : Widget
    {
        ConstructorInfo constructorInfo = typeof(T).GetConstructor(new[] { typeof(Widget) });
        Widget widget;
        if (constructorInfo != null)
        {
            widget = (Widget)constructorInfo.Invoke(new object[] { this });
        }
        else
        {
            Log.Error("Could not find constructor for type " + typeof(T).FullName);
            return null;
        }
        DockWidget dockWidget = new DockWidget(title, icon, this, typeof(T).FullName)
        {
            Widget = widget
        };
        DockWidgets.Add(dockWidget);
        Dock(dockWidget, dockArea, parent);
        return dockWidget;
    }

    /*
    public DockWidget ConstructDockWidget(string type, string title = "", string icon = null, DockArea dockArea = DockArea.Right, DockWidget parent = null)
    {
        Type dockWidgetType = Type.GetType(type);
        if (dockWidgetType == null)
        {
            Log.Error("Could not find type " + type);
            return null;
        }
        DockWidget dockWidget = new DockWidget(title, icon, this, type)
        {
            Widget = (Widget)Activator.CreateInstance(dockWidgetType)
        };
        if (dockWidget.Widget == null)
        {
            Log.Error("Could not create instance of type " + type);
            return null;
        }
        DockWidgets.Add(dockWidget);
        Dock(dockWidget, dockArea, parent);
        return dockWidget;
    }
    */

    public void LoadMap()
    {
		// This is a placeholder for now until I figure out how the asset browser works.
		// Pop up asset browser and filter by map

		var fd = new FileDialog( null );
		fd.Title = "Select VPK File";
		//Set a vpk filter

		if ( fd.Execute() )
		{
			Log.Info( $"File Selected: {fd.SelectedFile}" );
		}
	}

	public void OpenScene()
	{
		// This is a placeholder for now until I figure out how the asset browser works.

		var fd = new FileDialog( null );
		fd.Title = "Open Scene";
		//Set a scene filter

		if ( fd.Execute() )
		{
			Log.Info( $"File Selected: {fd.SelectedFile}" );
		}
	}

	public void BuildMenu()
	{
        // Top level menus (non-functional)
		Menu fileMenu = MenuBar.AddMenu( "File" );
		fileMenu.AddOption( "Open" ).Triggered += () => OpenScene();
		fileMenu.AddOption( "Save" );
        fileMenu.AddOption( "Save As" );
        fileMenu.AddOption( "Load Map" ).Triggered += () => LoadMap();
		fileMenu.AddOption( "Quit" ).Triggered += () => Close();
        Menu editMenu = MenuBar.AddMenu( "Edit" );
        editMenu.AddOption( "Undo" );
        editMenu.AddOption( "Redo" );
        // Toggle windows
        windowsMenu = MenuBar.AddMenu( "Windows" );
        for(int i = 0; i < DockWidgets.Count; i++)
        {
            windowsMenu.AddOption(DockWidgets[i].GetToggleViewOption());
        }
        // TODO: Add more menus
        Menu viewMenu = MenuBar.AddMenu( "View" );
        viewMenu.AddOption( "TODO" );
        Menu scriptsMenu = MenuBar.AddMenu( "Scripts" );
        scriptsMenu.AddOption( "TODO" );
        Menu helpMenu = MenuBar.AddMenu( "Help" );
        helpMenu.AddOption( "TODO" );
	}

	[Sandbox.Event.Hotload]
	public void OnHotload()
	{
        Clear();
        Close();
	}
}
