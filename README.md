# Extrapol
Nonlinear extrapolation in unity plus neat lighting and postprocessing  
> Project was made in 2018 and updated to Unity 2019.4.21f1  

Extrapolation code can be found here:  
[Extrapolations.cs](https://github.com/wndenis/Extrapol/blob/main/Assets/Resources/Scripts/Extrapolations.cs)  
[EnemyApproximator.cs](https://github.com/wndenis/Extrapol/blob/main/Assets/Resources/Scripts/EnemyApproximator.cs)



![Demo gif](https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/anim1.gif)  
---
### How it works:
It uses Newton's extrapolation by 3 samples to predict parabola.
> Note: sometimes it can miss. Missing probability rises with distance.  
> Project assumes that only one bullet is affected by gravity - the second one considered to be linear.

1) Sampling during first 0.5 seconds after shot  
![Sampling stage](https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/how1-min.jpg)
  
2) Extrapolating parabola (vector3 by time), iterating over time offset in seconds from 0 (last known target position) to 1 (position in 1 second after) using small step to find best solution.   
![Predicting stage](https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/how2-min.jpg)
  
3) Successfully hit target 😍  
![Target hit](https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/how3-min.jpg)
---
### Also look at:
* Water reflections, which I just imported from one good unitypackage
* Baked lighting
* Water particles
* Nice postprocessing
  
![PostProcessing](https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/PostProcessing.jpg)
<img src="https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/img1-min.jpg" alt="Demo 1" width="49.4%" />
<img src="https://github.com/wndenis/Extrapol/raw/main/ReadmeMedia/img3-min.jpg" alt="Water" width="49.4%" />
> Todo: embed video  
