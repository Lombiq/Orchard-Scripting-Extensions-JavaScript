# Orchard Scripting Extensions: JavaScript Readme



## Project Description

A child module for Orchard Scripting Extensions for running JavaScript code inside Orchard.


## Documentation

**This module depends on [JavaScript.Net Orchard](https://github.com/Lombiq/Orchard-JavaScript.Net) and [Orchard Scripting Extensions](https://github.com/Lombiq/Orchard-Scripting-Extensions). It uses many of the latter's features. Please install it first!** (And also read [that module's docs](https://github.com/Lombiq/Orchard-Scripting-Extensions) to see what you can do with it - and through it, with JavaScript).
JavaScript execution goes through the excellent [Javascript .NET library](http://javascriptdotnet.codeplex.com/).
Samples

	var hello = "Hello JavaScript!"; 
	hello; // The last statement will be the output of the script
	
	// You can instantiate types that were loaded to the script context through the Factory object
	var obj = Factory.Create("System.Object", null);
	obj.ToString(); // Outputs "System.Object"
	
	// There is an Orchard global variable that you can add fields to. By default it contains a WorkContext field with the Orchard WorkContext and an OrchardServices field with an IOrchardServices instance
	Orchard.WorkContext.CurrentSite.SiteName;
	
	// This adds the string "Hello!' to the markup of the layout's Body zone (this will just show up in the html source!).
	Orchard.Layout.Get("Body").Add("Hello!"); 

The module's source is available in two public source repositories, automatically mirrored in both directions with [Git-hg Mirror](https://githgmirror.com):

- [https://bitbucket.org/Lombiq/orchard-scripting-extensions-javascript](https://bitbucket.org/Lombiq/orchard-scripting-extensions-javascript) (Mercurial repository)
- [https://github.com/Lombiq/Orchard-Scripting-Extensions-JavaScript](https://github.com/Lombiq/Orchard-Scripting-Extensions-JavaScript) (Git repository)

Bug reports, feature requests and comments are warmly welcome, **please do so via GitHub**.
Feel free to send pull requests too, no matter which source repository you choose for this purpose.

This project is developed by [Lombiq Technologies Ltd](http://lombiq.com/). Commercial-grade support is available through Lombiq.