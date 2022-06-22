# Idea

I tried to create night sky skybox using simplex noise and hashes, and I've struggled a lot with stars flickering. So I decided to create shader based on noise modulated point mapping.
Basicaly it's just a sphere with (almost) equally distributed points on it, and each point is shifted a little. Point size is determined by fwidth to make star size be the same with different resolutions and camera FOV's. 

# Features
*  Fixed star size on different resolutions and FOVs.
*  Customizable stars density, size and brightness.
*  Customizable galaxy fog.
*  Customizable moon positioned by directional light.
*  Custom material inspector.

**Screens:**
https://gitlab.com/janmroz97/stars-skybox-shader/wikis/Procedural-stars-shader


# How to install

It's just Unity 2019.2.8f1 project. Open with Unity 2019.2.8f1 or newer.