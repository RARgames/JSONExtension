JSONExtension

IMPORTANT: How to create project for the first time:
1. Get missing references by installing them using NuGet Package Manager
2/ Add reference to System.ComponentModel.Composition 
3. To initialize MEF components, you’ll need to add a new Asset to source.extension.vsixmanifest.
```
<Assets>
	...
	<Asset Type = "Microsoft.VisualStudio.MefComponent" d:Source="Project" d:ProjectName="%CurrentProject%" Path="|%CurrentProject%|" />
</Assets>
```
