# AppInteriorDesign

New project with the necessary packages
 
 ### Unity version
  2022.3.27f1

# It has:

* Use API to authenticate Designer.
* GET info of the projects the user has and shows them.
* Navigate between scenes.
* Create a new project from space(device scanner) or a default room.
* Save objects locally.
* Save room as a json file but locally.
* Button edit takes json file from server, in this case, a local url, changing models.py: 
    unityproyect = models.FileField(upload_to='projects/', max_length=100) 
for getting a valid url, run your server locally and comment the line 27 in RoomLoader.cs 
and uncomment the line 28, put the url and ejecute the JsonScene.
* Models instantiated in a scene will be saved locally and will be loaded from there too.

# Some pending things:
* Still need to save objects and room json files in the firebase storage.
* Then take both json files from server and load into the JsonScene.
* Implement the virtual keyboard.
* Configure the WebScene.