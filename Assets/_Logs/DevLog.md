
# ============================== 4/12/26 ==========================
# -----------------Completed----------------
//More work on UI


# ----------------Next Steps----------------
1. UI to allow each part to change to another part from a list.
	[x] ~~(?) Obviously primarily this UI is for play mode, but should this UI work for edit mode too, or should a separate editmode UI be made?~~
		i. **One UI, different entry points - buttons on the inspector to enable/disable the singular UI**
	[ ] Figure out positioning of mech in screenspace, and how to handle vertical orientation of user screens
	[ ] A ui container that either shows a list of the current parts on the mech, or when editing a slot, shows the list of available parts for that slot.
	[ ] a script component that goes with each slot. (? Mech Puppet Slot Controller maybe already does everything I need)
	[ ] new sub-list struct of "available parts"
		i. Only one for now: "all parts." List determines what parts are available.
		ii. The UI should hold the reference to the current available parts list, and then filter based on which slot we are looking at.
	[ ] dropdown menu to select parts, should have the name, slot(s), and a picture of the part.
		i. going to need to figure out behavior for parts that take up multiple slots - and likely a filter to allow/disallow parts that use multiple slots ("chimera" parts).
2. Sprite property for parts
	[ ] Separate garage preview for current full sprite (built from parts)
	[ ] Sprite property for parts (array, so it can handle "chimera" parts)
	[ ] Color picker to customize colors of sprite

# ----------------Log today-----------------


# ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^END^^^^^^^^^^^^^^^^^^^^^^^^^^^^^# ============================== 4/11/26 ==========================
# -----------------Completed----------------
//Log file iteration, chatgpt questions / research
// Started messing with creating UI for part swapping.


# ----------------Next Steps----------------
1. UI to allow each part to change to another part from a list.
	[x] ~~(?) Obviously primarily this UI is for play mode, but should this UI work for edit mode too, or should a separate editmode UI be made?~~
		i. **One UI, different entry points - buttons on the inspector to enable/disable the singular UI**
	[ ] A ui container that either shows a list of the current parts on the mech, or when editing a slot, shows the list of available parts for that slot.
	[ ] a script component that goes with each slot. (? Mech Puppet Slot Controller maybe already does everything I need)
	[ ] new sub-list struct of "available parts"
		i. Only one for now: "all parts." List determines what parts are available.
		ii. The UI should hold the reference to the current available parts list, and then filter based on which slot we are looking at.
	[ ] dropdown menu to select parts, should have the name, slot(s), and a picture of the part.
		i. going to need to figure out behavior for parts that take up multiple slots - and likely a filter to allow/disallow parts that use multiple slots ("chimera" parts).
2. Sprite property for parts
	[ ] Separate garage preview for current full sprite (built from parts)
	[ ] Sprite property for parts (array, so it can handle "chimera" parts)
	[ ] Color picker to customize colors of sprite

# ----------------Log today-----------------


# ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^END^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

# ============================== 4/7/26 ==========================
# -----------------Completed----------------
//Created log file


# ----------------Next Steps----------------
1. UI to allow each part to change to another part from a list.
	a. new sub-list of "available parts", with the only one for now: "all parts." List determines what parts are available.
	b. dropdown menu to select parts, should have the name, slot(s), and a picture of the part.
		i. going to need to figure out behavior for parts that take up multiple slots - and likely a filter to allow/diallow parts that use multiple slots ("chimera" parts).
2. Sprite property for parts
	a. Seperate garage preview for current full sprite (built from parts)
	b. Sprite property for parts (array, so it can handle "chimera" parts)
	c. Color picker to customize colors of sprite
3. 
# ----------------Log today-----------------

Realized I have need for a devlog to keep track of my thoughts, and future plans.



# ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^END^^^^^^^^^^^^^^^^^^^^^^^^^^^^^