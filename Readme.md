# Orchard Scripting Extensions: JavaScript



## About

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


## Contributing and support

Bug reports, feature requests, comments, questions, code contributions, and love letters are warmly welcome, please do so via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.