# FFS _For Fu..._ **Fast File Search** :)

FFS was made as an experiment with NTFS and specifically MFT along with WPF in order to see how fast can the search be.. and it turned out to be pretty fast!!

# Introduction:
First step is scanning drive's master file table aka metadata
![1](https://user-images.githubusercontent.com/10902756/156497968-b1eb09e0-17be-440e-a15a-2f0623852430.png)

Once you're done with this all that's left to do is the actual file query
![2](https://user-images.githubusercontent.com/10902756/156498123-a1c245bf-8901-46b8-b6bd-97c52633a5c9.png)

# **The query options (green rectangle)** :
- \* - to display all files
- .ext - to display all files that match specific extension
- filename - regular string to display all filenames that match specific substring 

# **Stats (red rectangle)** :
- self explanatory, uses stopwatch

# **Toolbar options (blue rectangle)** : 
- Show selected file in explorer
- Open selected file with an associated program
- Export all the files to a CSV file

# **Files list (orange rectangle)** :
- that's where you can select a specific file you're interested in and perform various operations from the toolbar (see blue rectangle section)

Right now queries are very simple as they do what's needed for me hence why the class is called "Simple queries" however this bit should be abstracted so it shouldn't be too hard to add a new option such as JSON query with some sort of a Query builder maybe? Oh well the possibilities are endless!

# **List of things worth improvement** :
- Replace AdonisUI with some other fast, efficient and good looking WPF UI theme (I like adonis, you can't blame me..)
- Add more export options
- Look into context menu or some better interactions for more actions (context menus are borked thanks to adonis)
- Add more file search options that support Non-NFT drives (obviously it won't be as fast as MFT but it's going to make this software more suitable for using with other types of drives)
- Other options

**PR's are welcome!**
I don't plan to update or maintain this tool in near time until I have a need to do that... but feel free to open an Issue and I'll try to check it out if there's enough free time on my plate!


**Tools and frameworks used (Would've been a tough one without them ;) )** :
- https://github.com/benruehl/adonis-ui
- https://github.com/JoshClose/CsvHelper
- https://github.com/FantasticFiasco/mvvm-dialogs
- https://serilog.net/
