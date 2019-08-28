# <img src="wwwroot/images/icons/Example.png" width="50"> Example

### I really like building things with .NET Core

Everytime I'd spin up a project I'd have to scaffold Identity and setup user roles and create an admin page to manage those users, etc. It took a long time to get to actually building what I wanted to build. So, I spent some time to build this.

### What is this?

Glad you asked. So this is a Example with Identity scaffolded in, user roles already created, an admin page to manage those users/roles. It's still pretty bare bones, which is the point. It just has the stuff in it that I wanted to use to get started on a project.

### Tell me more.

Okay. So the way it works is you can clone this repo, do a restore to be safe, and then run it. The first user you register will be assigned the `ADMIN` role. Every other user after will get assigned the `USER` role. But there's a page you can navigate to to change those roles. There's a third role `SUPERUSER` which you could use as well. The page for managing users and roles is already restricted to the `ADMIN` role.

I have a commented code block for how to implement uploading and saving a user's image.

### Disclaimer
This is a simple, simple starter app just to have user authentication/authorization built it and ready to go. It's not perfect.
Also, there is an icon for default users, navbar, and favicon: me.
Also, also I've included WebEssentials.AspNetCore.PWA to get you on the road to a PWA; they're super cool. I suggest going to
https://github.com/madskristensen/WebEssentials.AspNetCore.ServiceWorker
to get started. I've already set it up and included the `manifest.json` file. You just have to edit it and add icons!