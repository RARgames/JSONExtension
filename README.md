JSONEx

**Find install-ready builds here: https://github.com/RARgames/JSONExtension/releases**


Main functionality:

* Open settings at Tools/JSON Extension Settings and select JSON path - language.json - with a structure declared in the file (especially "en" table)
	
* When you hover over any key you will see it's value in quick info
	
* If you want to edit, right click on the key and open JSON edit window. If you change the value/key, it will be automatically changed in language.json and every occurrence of old key will be replaced with new key in the whole solution.
	
* If the key is not existent, it will ask you to create new key



IMPORTANT: How to create project for the first time:
1. Get missing references by installing them using NuGet Package Manager
2. Add reference to System.ComponentModel.Composition 
3. To initialize MEF components, you’ll need to add a new Asset to source.extension.vsixmanifest.
```
<Assets>
	...
	<Asset Type = "Microsoft.VisualStudio.MefComponent" d:Source="Project" d:ProjectName="%CurrentProject%" Path="|%CurrentProject%|" />
</Assets>
```

Test project at: JSONExtension/test