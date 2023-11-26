/*
 *  Date:      November 11th, 2022
 *  Course:    CS 4540, University of Utah, School of Computing  
 *  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
 *    We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
 *    another source.  Any references used in the completion of the assignment are cited in my README file.
 *     
 *  File Contents:
 *  The Availability Tracker creates a page for the user to specify their availability during the weekdays. This file 
 *  utilizes the pixi library for creating interactable graphics across the screen, and data is grabbed and set to a 
 *  database in order to track this information. This page is only accessible to Applicants, because they need an application
 *  in order to be able to list their availability.
 */

$("#savedatamsg").hide();
$("#savebutton").hide();
$("#spinner").hide();

let bg_color = 0xabcdef;

let width = 1000;
let height = 1200;

//will be set to the data from GetSchedule
let slot_data = null;
let sourceID = 0;
let sourceX = 0;
let SourceAvailable = false;
let mouse_down = false;

app = new PIXI.Application({ backgroundColor: bg_color });
app.renderer.resize(width, height);
$("#canvas_div").append(app.view);

// ----------------------------------- Weekday + Time Text

const text = app.stage.addChild(
    new PIXI.Text("Monday", {
        fontSize: 24,
        lineHeight: 28,
        letterSpacing: 0,
        fill: 0xffffff,
        align: "center"
    })
);
text.anchor.set(0.5);
text.resolution = 8;
text.x = 220;
text.y = 50;

const text2 = app.stage.addChild(
    new PIXI.Text("Tuesday", {
        fontSize: 24,
        lineHeight: 28,
        letterSpacing: 0,
        fill: 0xffffff,
        align: "center"
    })
);
text2.anchor.set(0.5);
text2.resolution = 8;
text2.x = 340;
text2.y = 50;

const text3 = app.stage.addChild(
    new PIXI.Text("Wednesday", {
        fontSize: 24,
        lineHeight: 28,
        letterSpacing: 0,
        fill: 0xffffff,
        align: "center"
    })
);
text3.anchor.set(0.5);
text3.resolution = 8;
text3.x = 460;
text3.y = 50;

const text4 = app.stage.addChild(
    new PIXI.Text("Thursday", {
        fontSize: 24,
        lineHeight: 28,
        letterSpacing: 0,
        fill: 0xffffff,
        align: "center"
    })
);
text4.anchor.set(0.5);
text4.resolution = 8;
text4.x = 580;
text4.y = 50;

const text5 = app.stage.addChild(
    new PIXI.Text("Friday", {
        fontSize: 24,
        lineHeight: 28,
        letterSpacing: 0,
        fill: 0xffffff,
        align: "center"
    })
);
text5.anchor.set(0.5);
text5.resolution = 8;
text5.x = 700;
text5.y = 50;

for (let time = 8; time <= 20; time++) {
    const text = app.stage.addChild(
        new PIXI.Text(time + ":00", {
            fontSize: 24,
            lineHeight: 28,
            letterSpacing: 0,
            fill: 0xffffff,
            align: "center"
        })
    );
    text.anchor.set(0.5);
    text.resolution = 8;
    text.x = 800;
    text.y = 86 * (time - 7);
}

// ----------------------------------- End of weekday + time text


class SlotSquare extends PIXI.Graphics {
    on_color = 0x8cd667;
    off_color = 0xffffff;
    isAvailable = false;
    
    //ID, Day, Time, IsAvailable
    constructor(id, isAvailable, x, y) { //element.id, element.isAvailable, x, y
        super();
        this.id = id;
        this.isAvailable = isAvailable;
        this.x = 50 + 120 * x;
        this.y = 50 + 22 * y;

        this.interactive = true;

        this.draw_square();

        //add the SlotSquare object to the stage
        app.stage.addChild(this);

        this.on('mousedown', this.pointer_down);
        this.on('mouseup', this.pointer_up);
        this.on('mouseover', this.pointer_over);
    }

    draw_square() {

        if (this.isAvailable) {
            this.clear();
            
            this.beginFill(this.on_color);
            this.drawRect(0, 0, 100, 20);

        } else {
            this.clear();
            
            this.beginFill(this.off_color);
            this.drawRect(0, 0, 100, 20);
        }
    }

    pointer_down() {
        this.clear();
        this.isAvailable = !this.isAvailable;
        this.draw_square();
        mouse_down = true;
        sourceID = this.id;
        
        sourceX = this.x;
        SourceAvailable = this.isAvailable;
        //set data in list
        let x = this.id;
        let y = this.isAvailable;
        slot_data.forEach(function (element) {
            if (element.id == x) {
                element.isAvailable = y;
            }
        });
    }

    pointer_up() {
        $("#savebutton").show();
        $("#savedatamsg").hide();
        mouse_down = false;
    }

    
    pointer_over() {
        if (this.id != sourceID && mouse_down && this.x == sourceX) {
            this.isAvailable = SourceAvailable;
            this.clear();
            this.draw_square();
            let x = this.id;
            let y = this.isAvailable;
            slot_data.forEach(function (element) {
                if (element.id == x) {
                    element.isAvailable = y;
                }
            });
        }
    }
}

function save_data() {
    
    //prints the data set on the screen to the server
    //console.log(slot_data);
    console.log(slot_data);

    //$.post("Slots/SetSchedule", slot_data);
    $("#savebutton").hide();
    $("#spinner").show();
    var string = "";
    slot_data.forEach(function (item) {
        string = string + item.id + "," + item.isAvailable + ";";
    });
    console.log(string);
    $.ajax({
        type: "POST",
        url: "Slots/SetSchedule",
        data: {schedule:string},
        type: 'post',
        success: function () {
            $("#spinner").hide();
            $("#savedatamsg").show();
        }
    });

}


//Main Code, draw the table with data fetched from the database

$.get("Slots/GetSchedule", function (data) {
    slot_data = data;
    data.forEach(function (element) {
        let x = element.day;
        let y = element.time;
        new SlotSquare(element.id, element.isAvailable, x, y);

    });
});
