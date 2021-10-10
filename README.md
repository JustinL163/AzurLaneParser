# AzurLaneParser
Tool to parse data from AzurLaneTools JSON Repo into wiki format

Upon first run of the program, need to input 'y' to download the JSON data files when prompted. Also redownload the files when the game (/the JSON Repo) gets an update.

Once it finishes deserialising the JSON data, it will prompt for what ship to parse, the input options are;
    
    Input a ship name (Needs to be identical to the name in the files, other than not being case sensitive).
    
    Input the Group ID of a ship, e.g. Pamiat is 70202.
    
    The name of a .txt file, e.g. ship_list.txt, with one separate ship name or ID on each line, will parse all of them.
    
At the moment there are a few things missing before the program is completely functional, 

PR/META ships will refuse to parse, as they haven't been set up yet

Several ships that have unique equip options won't be written correctly https://azurlane.koumakan.jp/Template:Ship/Equipment/Switch the ones from here that have 
text like (on retrofit), (MLB), (LB1) etc will have incorrect gear currently, and Shinano and Graf Zeppelin will just fail

The attributes of modernisation nodes for retrofits won't yet output text like Torp Mount-1 MGM+1, "Skill" changes to "Skill+", Rarity changes to Ultra Rare etc

Skills that say stuff like "When sortied with any Cleveland-class ship" like Clevelad's skill etc won't be replaced with relevant links yet

Info like shipgroup (e.g. Child ship) and sub class (e.g. Helena has St. Louis subclass) is missing (idk if that's even from the files)

Collab ships not on EN won't parse atm, and there is not yet an option to take data from CN/JP files solely instead of EN files, like in the case of 
pulling stuff from CN predownload 

Drop location data isn't being parsed yet, though will be added later

Most other stuff should work mostly, though undoubtedly there will be bugs (like little bel's first skill doesn't populate correctly atm). 
Please tell me if you find other issues, I think you can put that stuff on here somewhere maybe? like in an issue or whatever. 

Or just DM me on ALO Discord Justin163#7721 UID: 521666259029458964
