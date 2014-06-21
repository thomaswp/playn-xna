Features
========

The following is a list of PlayN features and their current status.

Complete
--------
* Image, sound and text loading and use through Assets
* GroupLayer and ImageLayer should be fully functional
* CanvasImage is partially implemented (see below)
* Mouse/Keyboard input
* JSON
* Local and persisted Storage
* Audio for effects and music
* Text layout works in many cases (see below)

Incomplete
----------
* Gradients, Clipping
* Canvas is only partially complete, allowing fills but not draws. Drawing paths and path clipping are not yet supported.
* ImmediateLayer/Surface are only partially completed
* Text layout is still rather buggy and inconsistent. Because XNA has limited text measurement, a custom solution may have to be implemented, such as that for HTML text measurement.
* SurfaceImage is not implemented.
* Keyboard support lacks a perfect mapping from PlayN to XNA keys
* Net only allows outgoing webpage requests

Unsupported
-----------
* OpenGL in general, specifically shaders. The GLContext will always return null. However, you're welcome to write your own shader in XNA to do the job.
* Multithreaded resource loading is not supported in XNA. Async calls will block other resource loading, but it will not block game code.
