from bs4 import BeautifulSoup
import pandas as pd
import requests
import time


url = "https://docs.microsoft.com/ja-jp/dotnet/api/system?view=netframework-4.8"
r = requests.get(url)
time.sleep(3)

soup = BeautifulSoup(r.text , 'html.parser')
# print(soup)
#contents = soup.find(class_="nameValue table table-sm table-stacked-mobile")
contents = soup.find(class_="content")

get_a = contents.find_all("a")
url_head = "https://docs.microsoft.com/ja-jp/dotnet/api/"
count  = 0

# print(get_a)
title_links =[]
for i in range(len(get_a)):
	
	try:
		link_ = get_a[i].get("href")
		title_links.append(url_head +link_)
		print(url_head + link_)
	except:
		pass
	
	time.sleep(0.5)

df = pd.DataFrame(title_links)
df.to_csv("url.csv",index = False,encoding="utf-8")

# print(title_links)
# class_links =[]
# class_titles = []

# for i in range(len(title_links)):
#	tmp_link = title_links[i]
#
#	r = requests.get(tmp_link)
#	time.sleep(3)



#	soup = BeautifulSoup(r.text , "html.parser")
#	youtube_title = soup.find(class_ = "entry-title").text
#	print(youtube_title)
#
#	if youtube_title == "404 NOT FOUND":
#		continue
#	else:
#		class_titles.append(youtube_title)
#		youtube_link = soup.find("iframe")["src"].replace("embed/","watch?v=")
#		class_links.append(youtube_link)

# print(youtube_links)
# result = {
#	"youtube_title": class_titles,
#	"youtube_link" : class_links
#	}
# df = pd.DataFrame(result)
# df.to_csv("result.csv",index=False,encoding="utf-8")