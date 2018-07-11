# Step One: Packaging the build
- [ ] Open the build settings menu
- [ ] Verify that the builds target is set to Windows on a x86_64 architecture
- [ ] If the build is a development build, tick the checkbox for this.
- [ ] Open player settings
- [ ] Verify that the product name and company name are set to 'ProjectOrbis' and 'Lucian' respectively
- [ ] Update splash screen settings if necessary
- [ ] Go back to the build settings menu and select build

**THE BUILD SHOULD BE PLACED INTO THE FOLDER WITH AN APPROPRIATE VERSION NAME**

# Step Two: Build checks
After building the game, go and check the following things:
- [ ] That the executable game actually runs
- [ ] Check for any basic bugs or obvious problems, if you find any take note of these. If any of these bugs cause the game to be not playable, this build should not be released.

# Step Three: Packing the build files
- [ ] In the build folder create a zip archive or zip folder and name with the release version eg `v1.1.0.zip`

Your folder structure should now look like the folowing:
```txt
.../My Builds/v1.1.0
            Mono/
                    .....stuff
            v1.1.0_Data/
                    .....stuff
            v1.1.0.exe
            UnityCrashHandler64.exe
            UnityPlayer.dll
            v1.1.0.zip/
                    this folder is empty
```
- [ ] Copy all the files and paste them into the zip folder
- [ ] Rename the game exe to 'Project Orbis'
- [ ] Rename the game_Data folder to 'Project Orbis_Data' **This is critical**
- [ ] Now open the data folder and navigate into 'StreamingAssets'
- [ ] Clear the streaming assets folder of any files **DO NOT OPEN THE GAME AFTER THIS POINT**
- [ ] Navigate back to the root of the zip file and create a 'NOTICE.md' file. This is a markdown file and 
should contain a list of all changes and fixes in this release and **must** following the following template:
```Markdown
# NOTICE
Write a small summary of the controls here along with any critical reminders or information you 
want the player to know about.

# Additions
- a list
- of additions
- should be
- placed here

**Note: An addition is different to a fix. An adition adds a new feature, a fix just changes an 
already existing feature.**

# Fixes
- A list
- Of fixes
- Should be
- Placed here

# Known Issues
Write about any know bugs with this current version here. Hopefully this should lessen the number of people 
telling you about problems you are already aware about.
Copy and paste the following line below this section: 
**IF YOU FIND ANY BUGS NOT LISTED ABOVE, FEEL FREE TO REPORT THEM [HERE](https://github.com/TheLetsCodeProject/ProjectOrbis_2/issues/new/choose)**
```
Your folder structure of the zip file should now look like the following:
```txt
.../My Builds/v1.1.0/v1.1.0.zip/
            Mono/
                    .....stuff
            Project Orbis_Data/
                    .....stuff
            Project Orbis.exe
            UnityCrashHandler64.exe
            UnityPlayer.dll
            NOTICE.md
```

# Uploading to github
- [ ] Open chrome and navigate to the project orbis github page.
- [ ] Go to the releases section and select 'Draft New release' (top right corner)
- [ ] Name the release with something relevant. Probably the version name followed by the release type eg `v1.1.0-Alpha.1 [PRE RELEASE]`. To see more examples just look at some of the already relased builds.
- [ ] Copy and paste the text from your NOTICE.md file into the release notes section
- [ ] If the build is a PRE RELEASE, click the relevant checkbox.
- [ ] In the 'Attach binaries' section, upload the zip file you created in the earlier steps.
- [ ] Publish the release **THIS IS IMPORTANT. GITHUB WILL NOT AUTOMATICALLY SAVE YOUR RELEASE DRAFT**
