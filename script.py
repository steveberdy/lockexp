import os
import json

FAKE_TITLE = "Announcing Windows 12!"
FAKE_URI = "microsoft-edge://?url=https://www.youtube.com/watch?v=dQw4w9WgXcQ"
FAKE_ACTION_TEXT = "Get access now!"
FAKE_DESCRIPTION = "It's like Windows 11, but if people actually wanted it"

content_parent = f"{os.environ["LOCALAPPDATA"]}\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\TargetedContentCache\\v3"

for child in os.scandir(content_parent):
    if not os.path.isdir(child):
        continue
    
    for file_name in os.listdir(child):
        file = os.path.join(content_parent, child, file_name)
        content = json.load(fp=open(file, "r", encoding="utf-8"))

        if content["name"] != "LockScreenOverlay":
            continue

        print("CONTENT SOURCE FOUND: " + file)

        for i in range(len(content["items"])):
            content["items"][i]["properties"]["title"]["text"] = FAKE_TITLE
            content["items"][i]["properties"]["description"]["text"] = FAKE_DESCRIPTION
            content["items"][i]["properties"]["actionText"]["text"] = FAKE_ACTION_TEXT
            content["items"][i]["properties"]["onClick"]["parameters"]["uri"] = FAKE_URI
            content["items"][i]["properties"]["onClick"]["parameters"]["actionText"] = FAKE_ACTION_TEXT
            content["items"][i]["properties"]["onClick"]["parameters"]["description"] = FAKE_DESCRIPTION
            content["items"][i]["properties"]["onClick"]["parameters"]["title"] = FAKE_TITLE
        json.dump(content, fp=open(file, "w"))