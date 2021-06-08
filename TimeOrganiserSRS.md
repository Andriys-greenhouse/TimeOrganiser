<center> <h1> Time Organizer</h1> </center>
<center> <h2> software requirements specification </h2> </center>

<br/><br/><br/>

<center> <h4> Version </h4> </center>
<center> 1 </center> 
<br/>

<center> <h4> Overview </h4> </center>
<center> This version is the first outline of the project and <br/> it is created mainly for educational purposes. </center>
<br/>

<center> <h4> Creation date </h4> </center>
<center> 2021/05/17 </center>
<br/>

<center> <h4> Authors </h4> </center>
<center> Ondřej Samsounek </center>

<br/><br/><br/><br/>

### 1. Introduction
1. **Purpose** <br/> 
Purpose of this document is to specify functions of desktop application called Time Organizer which could be helping its users to plan their activities and to record and sort their tasks by set criteria.

1. **Document conventions** <br/>
Final product of development is sometimes in this document called product and sometimes application. <br/><br/>
In part 4. System properties, individual properties are described like this:
    
  1. **Property Name**
    
      1. Description of property
        
      1. Input-Action-Result

1. **Intended audience** <br/>
Reader of this document should have basic experience in using PC and should have a connection to internet to search for concepts which he haven't yet encountered.

1. **Contacts** <br/>
Ondřej Samsounek - samsounek.2018@skola.ssps.cz

1. **References to other documents** <br/>
This document is the first one and so far the only one. In the future Function specification document and Technical Specification document might be written.
    
<br/>
### 2. Overall description 
1. **Product perspective** <br/>
Product will help user organize his time by allowing him to build his "time bar" with starting point and which will be divided into segments with title, color and duration. Other part of application will handle tasks of user and will sort them by criteria modifiable by user in settings.

1. **Product function** <br/>
Product will allow user to make his own segments, tasks, "time bars" and settings. Abbreviated informations about them will appear in main window of application and user will be able to access detailed informations from there too.<br/>
Evaluating and sorting of tasks will be done by application automatically. <br/><br/>
Product will load and save created segments, created tasks, last "time bar" and users settings after every change done to them through app. This data will be saved as text into four separate .txt files created automatically by program. Text files shall be created in the same directory, where .exe file of program will be placed. User won't be able to modify these records directly, they will be accessed only through the app. Therefore records will be encrypted with XOR cipher by fixed cay before saving into file not to be easily modifiable manually.

1. **User classes** <br/>
Time Organizer is designed to be used mostly by people with some experience with computers for recording simple tasks. Program is not going to be in first versions too visually appealing and its use might be at first unintuitive for people who haven't used form apps before.

1. **Operating environment** <br/>
Program will be designed to run on computers with Windows OS. Its code will be written in C# and WPF and finally compiled to .exe file.

1. **User design** <br/>
In main window user shall see two representations of "time bar". The first will be the "detail bar", it will display horizontal list of tiles with title and duration (in format from-to) of each segment. The second will be "real bar" which will display length of each segment compared to other segments. <br/>
Next element will be list view of created tasks, where each element will have title, importance, deadline and checkbox recording whether the task is done or not. <br/>
Next to list view of tasks details about selected task are going to be displayed. <br/>
The final components of main window are going to be four buttons which will:
    - create new task
    - create new bar
    - open settings
    - delete all solved tasks
  
  Creation of new tasks (created from main window) and segments (created from new bar window) will be done in simple form with add and cancel buttons on the bottom and error reporting hidden line under every text box. <br/>
  Creation of new bar will be done by "adding" from one list view of all existing segments into other one containing list of segments of new "time bar". This adding will be implemented by buttons which will add selected object from first to the second one.

1. **Design limitations and implementation** <br/>
Product shall not have any backups of saved data, if files will be deleted or data will get corrupted, application will notice user but will not be able to recover any data. <br/><br/>
Limited inputs:
    - Segment length must be at least 10 minutes and in total length of all segments must be less than 25 hours (1440 minutes). It than follows, that segment must be shorter than 1440 minutes and so the upper limit will be set to 1410 minutes.
    - Separating segment (segment automatically added between segments and giving user time to get ready for next segment) must be set either to 0 minutes (for non) or to number between 10 and 30 minutes.
    - Tasks deadline must be in the future (from the point of creation).
	- Task and segment title must have from 2 to 20 characters.
	- Task and segment description must have from 2 to 250 characters.
	- Color of segment will be picked from possibilities presented by application.
	- "Time bar" must contain at least one segment.

  Entered values not matching these conditions will be considered as invalid input. <br/><br/>
  No language settings will be provided, user must control the program in English. <br/><br/>
  System will have no way of verifying user identity, unauthorized user will be able to make same changes as rightful owner.

<br/>
### 3. Interface requirements
1. **User interface** <br/>
User of product must have basic knowledge of how to work with form applications an with PC in general. <br/><br/>
User won't try to sabotage the program by adding very large numbers of new objects and won't try to damage files with saved data. <br/><br/>
User must be able to control the application in English.


1. **Hardware interface** <br/>
The application will have available at least 200 MB of RAM memory while running. <br/><br/>
There will be at least 20 MB free on devices hard drive.<br/><br/>
GPU and CPU of device running application will be able to run simple WPF form app.

1. **Software interface** <br/>
Device running the application will have installed Windows 8 or newer version of Windows OS.


<br/>
### 4. System properties
1. **Create new bar**
    1. User will be able to create new bar from existing segments or to create new segments in window appearing after clicking button in main window with this description.
    1. Button click (in the main window) - appearance of dialog window - input of tasks to list view - verification - (display error message) - close the window, save to last bar and update main screen
    
1. **Create new segment**
    1. User will be able to create new segments to chose from while making a bar.
    1. Button click (in Creating new bar window) - appearance of dialog window - input of values - verification - (display error message) - close the window, update list of segments and save all segments
    
1. **Create new task**
    1. User will be able to create new task to be handled and displayed by application.
    1. Button click (in the main window) - appearance of dialog window - input of values - verification - (display error message) - close the window, update list of tasks and save all tasks
    
1. **Evaluation of tasks**
    1. In Settings user will be able to set coefficients (here X and Y) into following formula: <br/> 
    "Value" of task = Importance of task * X - Days to deadline * Y <br/>
    This value shall than be used to determine tasks position on the list.
    1. Impulse to update - sort list of tasks by evaluation formula - (update content of main window)
    
1. **Settings**
    1. User will be able to set here criteria for task sorting and length of separating segment. Criteria for task sorting will be modified by inputting coefficients into formula counting "value" of task.   
    1. Button click (in the main window) - appearance of dialog window - input of values - verification - (display error message) - close the window, update main window and save to settings
    
1. **Display details**
    1. By double click on task or segment in bar, user will open a dialog window in which he will be able to see and edit details about selected object.
    1. Double click on task or segment - appearance of dialog window - input of values - verification - (display error message) - close the window, update list from which element was selected save modified data

<br/>
### 5. Non functional requirements 
1. **Security** <br/>
Product won't verify users identity. <br/><br/>
Product will save needed data encrypted with XOR cipher with fixed cay to make manual editing harder.

1. **Reliability** <br/>
Product should be able to start and run without files into which data were saved to.<br/><br/>
In case of detecting a corrupted line in saved data system won't evaluate all file as corrupted but will continue loading and will inform user about this incident.

1. **Project documentation** <br/>
Other documents will be written after this one will be approved. If required status reports can be written while development will be in progress.
 
1. **User documentation** <br/>
User documentation document will be composed after product shall be finalized to pre-release state and tested successfully.
