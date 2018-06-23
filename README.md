# Unturned-Plugin
An example on how to make your OWN plugin.

# Usage
1. Download all files and extract
2. Open Plugin.sln in Visual Studio
3. Verify that the references are valid
4. Build the project
5. In your servers Unturned install folder, subdirectory Modules, create a folder called "Plugin"
6. Copy the following files from the build output (Plugin\bin\Debug) into the new "Plugin" folder:
     - English.dat
     - Plugin.dll
     - Plugin.module
     - Plugin.version
7. Run your server, within the first few lines of starting up you should see "PLUGIN LOAD" surrounded by dashes for ease.
8. Congratulations, you can now build up your own plugin that relies on you and only you.

# Information
This is a PROOF OF CONCEPT project.
No license or any restriction has been placed on you, this relies on your own ability and it's your freedom on how you work it.

If you'd like to add more function hooks you can use the Visual Studio reference explorer to view through the Assembly-CSharp.dll in your Unturned_Data->Managed folder and see all the functions and classes that *apparently* we're too dumb to find for ourselves. Good luck out there :^)

Check out the [Wiki](https://github.com/FCheat/Unturned-Plugin/wiki) for tutorials like Adding your own event hooks for things like when a player joins.

# Images
![Plugin Load](https://i.imgur.com/LIdTx6f.png)

![Help Command](https://i.imgur.com/0xKuvPi.png)

![Website Command](https://i.imgur.com/5gqyazu.png)
