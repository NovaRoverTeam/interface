Welcome to the Nova Rover Team Unity Interface

The purpose of this application is to provide a user interface for the cameras onboard the Nova Rover Sandstorm Model D(arude).
The application is fairly simple and is based on the premise that the remote cameras will be mounted as USB webcams.
This is done using the free program USB redirector

The main screen of the application contains several scenes.
It is recommended that you open the settings menu and tailor the settings to the task at hand.
The top camera should be the 360 degree camera you are using (Ricoh Theta S) - be mindful that you need to download the Ricoh UVC blender to make this work
The second camera can be toggled on and off and then selected from another dropdown menu
Click apply for these settings to be saved and to go back to the main menu

Click Start

You are now viewing the Ricoh Theta S USB stream. The video quality is not as sharp as it could be and there is a bad interface between the two
hemispheres. Feel free to improve this....
There are several elements in this view. There is the ricoh stream which surrounds the viewer. There is a picture in picture rectangle for viewing a second webcam (PIPscreen)
and a field for bearings from the rover. There is also a compass and toggleable pointer that surrounds the user. These elements interface with the ROS nodes on the rover.

While viewing the spherical video stream there are several hotkeys that may be useful
Esc = exit the application
Backspace = go back to main menu
A key = rotate the PIPscreen counterclockwise
D key = rotate the PIPscreen clockwise
Spacebar = toggle the PIPscreen on and off
Up key = scroll up through USB devices
Down key = scroll down through USB devices
Enter key = Takes the currently selected USB device and projects it onto the PIPscreen
Note: If the selected device is the same device as the one projected on the sphere, enter will not work

I hope you enjoy my baby

Note: If you open the application with an oculus rift plugged in, it should pretty much just work. Hotkeys are most useful for when you have the oculus plugged in as the dropdown menu is hard to see and interact with
