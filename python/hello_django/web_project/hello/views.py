import re

from django.shortcuts import render
from django.http import HttpResponse
from django.utils.timezone import datetime

# Create your views here.
def home(request):
    return HttpResponse("hello, Django")

def hello_there(request, name):
    now = datetime.now()
    formatted_now = now.strftime("%A, %d %B, %Y at %X")

    # filter the name argument to letters only using regular expression
    # URL arguments can contain arbitrary test, so we restrict to safe chars only
    match_object = re.match("[a-zA-Z]+", name)

    if match_object:
        clean_name = match_object.group(0)
    else:
        clean_name = "Friend"

    content = "Hello there, " + clean_name + "! Its " + formatted_now
    return HttpResponse(content)