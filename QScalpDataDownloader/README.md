# QScalpDataDownloader
QScalp data downloader from site http://erinrv.qscalp.ru/

You must enter file name that contents data in "input.txt" file.

Examples of files with data you want download in "examples" folder.

Launch script you can in bin>Release. Ensure that you edit files "input.txt" and "{your file name}""

Format:
1) {instrument name pattern};{instrument name pattern};...
2) {begin expiration date}; {end expiration date}

Description:
1) First string contents patterns of instrument names. For instruments with expiration date, you must add "-{expiration month end}".
2) Second string contents start and end expiration dates. For instruments without expiration date you can chose time period whatever you want.
