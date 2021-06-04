# Unity CardboardVR Power Tools

This project contains a set of tools for Unity to streamline development for Cardboard VR.

Those tools are prepared to encapsulate the logic and utilities provided by the [Cardboard XR plugin](https://github.com/googlevr/cardboard-xr-plugin.git).

The tools included in this project are:

- 4 Types of cameras with their own pointer and loading effects.
  - Static camera
  - Restricted Movement by spots camera
  - Free movement camera
  - Menu camera

- Components with specific funcionalities
  - Component to allow interaction with the VR cameras.
  - Component to display text on the screen.
  - Component needed for the restricted movement camera to designate allowed spots

- Simulator for VR in Editor (Allowing WebGl builds at no extra work)

---

## The Four Cameras

### **· BaseVRCamera**

Found on */Cameras/BaseVRCamera*.

This camera can work on it's own as a camera fixed in a position, it contains all basic functionalities. All other cameras are built on top of this one.

Properties that could be changed are:
- Active VR Interactions: When set to false it doesn't interact with other gameobject.
- Pointer Distance: Max distance at which it will interact with gameObjects.
- Base Color: Color of the pointer when no object is being pointed at.

![BaseVrCameraProperties](/ReadmeImgs/BaseVRCameraProperties.png)

### **· SpotMovementVRCamera**

Found on */Cameras/SpotMovementVRCamera Variant*.

This camera needs of the existence of elements in the scene with the **SpotDestination** component. After pointing at them for a little duration the camera will move towards the indicanted spot.

Properties that could be changed are:
- Speed: Speed at which the camera will move from spot to spot.

![SpotMovementVRCameraProperties](/ReadmeImgs/SpotMovementVRCameraProperties.png)

#### Preparing a Destination Spot:
1. Select the gameObject that will work as the spot.
1. Add the SpotDestination component.
1. Add the VRInteraction component.
1. Drag the SpotDestionation component to the OnPointerLoad event.
1. Add the *SpotDestination.startCameraMovement* call on the OnPointerLoadEvent.

*It has the VRInteraction properties*

![SpotDestinationProperties](/ReadmeImgs/SpotDestinationProperties.png)

### **· FreeMovementVRCamera**

Found on */Cameras/FreeMovementVRCamera Variant*.

This camera will move forward as long as the trigger is being pressed. Even though possible, it is not recomended to add OnClick Event intractions on gameObjects if this camera is being used.

Properties that could be changed are:
- Speed: Speed at which the camera will move.

![FreeMovementVRCameraProperties](/ReadmeImgs/FreeMovementVRCameraProperties.png)

### **· MenuCamera**

Found on */Cameras/FreeMovementVRCamera Variant*.

This camera allows interaction with UI elements. All those elements should be placed in the canvas "MenuCanvas". Elements of the canvas should be placed vertically, so selection is done by looking up or down.

![MenuVRCameraHierarchy](/ReadmeImgs/MenuVRCameraHierarchy.png)

Properties that could be changed are:
- FollowCamera: Whether the canvas will follow the camera's Y Rotation (staying always in sight) or it will stay fixed with the initial rotation.

![MenuVRCameraProperties](/ReadmeImgs/MenuVRCameraProperties.png)

---

## The Four Events - VRInteraction

All objects with which the camera can interact must have the VRInteraction component. Such component has 4 events to which diferent methods can be added.

### **· OnPointerEnter**

Triggers when the VRPointer (center of screen) points towards the object and the Distance set to the pointer is greater or equal than the object's distnce.

### **· OnPointerExit**

Triggers after the VRPointer stops pointing towards the object.

### **· OnPointerClick**

Triggers when the Cardboard button (or mouse left click) is pressed

### **· OnPointerLoad**

Trigger after the specified time looking at the object has happened. During the whole process a loading pointer is drawn to show the user how much time is left for the trigger to happen.

### Properties

- LoadTime: Time the user has to be pointing at the object for the OnPointerLoad event to trigger.
- HoldColor: Color the pointer will change to when pointing at the object (it tells the user if objects are interactable and in valid distance to be interacted with).
- ClickColor: Color the pointer will change to when OnPointerClick event is triggered.

![VRInteractionProperties](/ReadmeImgs/VRInteractionProperties.png)

---

## Configuring the Project

First of all, follow the steps specified in the [Quickstart for Google Cardboard for Unity](https://developers.google.com/cardboard/develop/unity/quickstart), since all tools interact with the given package.

In your scene, remove the main camera and add in it's place one of the four VR cameras you can find in this package (Camera specifics are detailed below).

Add **VRInteraction** Component to all gameObjects that are expected to interact with the camera. And link the methods you are interested to excute when the events are triggered.

You're done!

### Controls in non-VR mode (Editor / WebGL)

- Press "Z" for the camera rotation to track the mouse movements.
- Use the left mouse button as the trigger.
