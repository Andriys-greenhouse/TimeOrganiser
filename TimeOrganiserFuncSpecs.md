[//]: # (sorry for the state of text, it is made in hurry)
<center> <h1> Time Organizer</h1> </center>
<center> <h2> Function Specification </h2> </center>

<br/><br/><br/>

<center> <h4> Version </h4> </center>
<center> 1 </center> 
<br/>

<center> <h4> Overview of version</h4> </center>
<center> This version is first document describing designed software <br/> from user's perspective. <br/> (created mainly for educational purposes)</center>
<br/>

<center> <h4> Creation date </h4> </center>
<center> 2021/05/26 </center>
<br/>

<center> <h4> Authors </h4> </center>
<center> Ondřej Samsounek </center>


<br/><br/><br/><br/>


### 1. Introduction
1. **Purpose** <br/> 
This document is intended to describe user interface and reactions of designed software in more detail. For description less focused on this  

1. **Document conventions** <br/>
Final product of development is in this document called product, application or simply app. 

1. **Intended audience** <br/>
Reader of this document should have basic experience in using PC and form apps and should have a internet connection to search for concepts which he haven't yet encountered.

1. **Contacts** <br/>
  * Ondřej Samsounek
     * email: samsounek.2018@skola.ssps.cz
     * tel: +420 123 123 123

1. **References to other documents** <br/>
First outline of designed software was described in SRS, where reader can find less concrete description. If you will need this document, contact some of personal mentioned above.
    
<br/>
### 2. Scenarios 
1. **Use cases** <br/> 
User can use application in one of three ways:
  * Time organizer (using only bar and segment features) <br/><br/>
  * Task manager (using only task recording and evaluation)<br/><br/>
  * Both (using all apps features)

1. **Personas - types of user roles** <br/> 
In this stage of development implementation of different user roles isn't planned. If user will have .exe file of application on his computer, he can use it without restriction.

1. **Delimitation of the scope** <br/> 
  * **Data management** <br/> Data will be loaded and saved to files at launch and closing of the application but also at any manipulation with, or addition to existing data (new object, modification of existing,...). <br/><br/> In first versions if user wishes to transfer his data from device to device, he'll have to transfer his .txt files and place them in the same file as .exe file of application. The same goes for two users using one application - both of them will copy their files to separate folders after using the app, before the other user will replace his files in folder with app by his own. But this case is highly improbable due to small size of application and therefore possibility to make two copies of .exe file of app into separate folders. <br/><br/> If .txt files with app data will be renamed or deleted, application will only notify user about this incident via message box and create new ones with default values but won't be able to recover his files. <br/><br/> If the application will, while loading, find a corrupted line or lines in .txt files it will count how many lines were corrupted and after loading whole document will notify user about this incident. <br/><br/>
  * **Encryption** <br/> Text in determined format (with separators and line splits) will be saved into .txt files encrypted by [XOR cipher](https://en.wikipedia.org/wiki/XOR_cipher) using key, firmly set in souse code of application. Decryption while loading data from files will be done by the same key. <br/><br/>
  * **Keyboard shortcuts** <br/> Unless testing of application will show, that keyboard shortcuts pose a real threat to running of application none of them won't be implemented nor banned except ctrl + s, which will save and load data, which application will work with. <br/><br/>
  * **Settings** <br/> Settings will contain only setting of length of separation segment and fields to fill in days left and importance coefficients. <br/><br/> Settings window won't (at least in first versions) contain font size control feature.<br/><br/>
  * **Manual** <br/> User manual will be implemented as a dialog window (accessible from main window), which will contain brief description of windows and controls found in them.

1. **Areas with low emphasis** <br/> 
  * **Security** <br/> Application won't pack any safety features like login or solid encryption of files. They are considered as unnecessary but can be added in later versions. <br/><br/>
  * **Performance** <br/>
  On most computers application should run smoothly without putting too much load on processor, RAM or GPU. This is why not too much emphasis is put on this area.


<br/>
### 3. Rough overall architecture
1. **Workflow** <br/> 
Described in attached PDF.

1. **Main modules and details** <br/> 
 * **Main window** <br/>
   In main window most of information and functionalities will be focused and other windows will serve as means of editing in or adding to it. <br/><br/>
     * **Main components:**<br/><br/>
     **Detail bar** - bar showing segments with their title and time period (from-to). <br/> 
     **Real bar** - bar showing actual length of segments compared with others. <br/> 
     **Task list** - list of tasks sorted by criteria given in settings, every element shall have title, deadline, importance and checkbox marking weather task is finished <br/> 
     **Details display** - it shall be next to Task list and will show details (title, description, importance and deadline) about selected task  <br/> 
     **Buttons** - Delete all solved tasks, settings, Manual, New bar and New task buttons <br/><br/>
     * **Message boxes:<br/><br/>Files not found** - I couldn't find your files so i created new ones with default values.<br/>**Corruptions detected** - I found X corrupted lines and deleted them, please have a look at your objects and recreate the missing ones.<br/>**Delete confirmation** - Are you shure, that you want to permanently delete X tasks?<br/><br/>
 * **New task and edit task window<br/>**This window shall be used to modify existing tasks and to gather data to pass to newly created tasks.<br/><br/>
     * **Main components:**<br/><br/>
     **Text boxes** - Before each one of them will be text describing which information belongs into it (title, description, importance and year, month, day and hour of deadline) and under every of them will be invisible line with error text, which will appear after attempt to submit if it is needed.<br/>
     **Buttons** - Add / Change and cancel buttons.<br/><br/>
     * **Message boxes:**<br/><br/>
     **Invalid values entered** - Some fields were filled by invalid data.<br/><br/>
 * **New bar window**<br/>
 From New bar window, user will be able to create new segments and to arrange segments to new tasks segment list. Also he will be challenged to fill in hour and minute of bars start.<br/><br/>
     * **Main components:**<br/><br/>
     **List of existing segments** - List will display already created segments. At double click on segment editing window of that segment will open.<br/>
     **List of segments of new bar** - Using buttons user will add segments selected by him in List of existing segments one by one into this list.<br/>
     **Buttons** - -> and <- buttons are going to add and subtract from List of existing segments,<br/> Add and Del buttons will be used to add new and to delete existing segments,<br/> Create and Cancel buttons are going to be used to confirm or to cancel creation of new bar.<br/>
     **Text boxes** - before each one of them will be text describing which information belongs into it (hour and minute at which bar starts) and under every of them will be invisible line with error text, which will appear after attempt to submit if it is needed.<br/><br/>
     * **Message boxes:**<br/><br/>
     **Bar too long** - Bar entered by you and starting at point you entered exceeds in the end 24 hours.<br/>
     **Invalid values entered** - Beginning fields were filled by invalid data.<br/><br/>
 * **New segment and edit segment window<br/>**This window shall be used to modify existing segments and to gather data to pass to newly created segments.<br/><br/>
     * **Main components:**<br/><br/>
     **Text boxes** - Before each one of them will be text describing which information belongs into it (title, description and duration) and under every of them will be invisible line with error text, which will appear after attempt to submit if it is needed.<br/>
     **Buttons** - Add / Change and cancel buttons.<br/>
     **Color selection box** - In this box user will be able to select from offered colors.<br/><br/>
     * **Message boxes:**<br/><br/>
     **Invalid values entered** - Some fields were filled by invalid data.<br/><br/>
 * **Settings window**<br/><br/>
 Will be used to enter length of separating segment (segment between user entered segments in bar) and evaluation coefficients.
     * **Main components:**<br/><br/>
     **Text boxes** - Before each one of them will be text describing which information belongs into it and under every of them will be invisible line with error text, which will appear after attempt to submit if it is needed.<br/>
     **Buttons** - OK and cancel buttons.<br/><br/>
     * **Message boxes:**<br/><br/>
     **Invalid values entered** - Some fields were filled by invalid data.<br/><br/>
 * **Manual window** <br/>
 Manual window shall display determined text with brief overview of information described in this part. Possibility of adding a OK button is considered.<br/><br/>
 * **Save and load sequences**
 These will include encryption and decryption.