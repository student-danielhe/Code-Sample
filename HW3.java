package cs3540;

import processing.core.PApplet;
import java.util.Random;
/**
 * Your work in this class
 * do not change the name of the class
 * @author (Your Name)
 */
public class HW3 extends PApplet {
	
	// Do not change
	public static void main(String[] args) {
		// Tell processing what class we want to run. 
		PApplet.main("cs3540.HW3");
	}

    // method for setting the size of the window
	  private int ballX=250;
	  private int ballY=200;
	  private int ballSPDX=0;
	  private int ballSPDY=3;
	  private int rectX=0;
	  private int rectSPD=0;
	  private int R=0;
	  private int G=0;
	  private int B=0;
	
	public void settings() {
			// If using eclipse the size of our applet must be set her and NOT in setup.
			size(500, 400);
		}
	public void setup (){
	  size(500,400);
	  
	}

	public void draw(){
	  background(255);
	  rectSPD=mouseX-rectX;
	  if(rectSPD>5){
	  rectSPD=5;
	  }
	  if(rectSPD<-5){
	  rectSPD=-5;
	  }
	  rectX=mouseX-50;
	  if(abs(ballX+ballSPDX-mouseX)<50&&ballY+ballSPDY>360){
	    ballSPDY=-3;
	    ballSPDX+=rectSPD;
	  }
	  if(ballY<ballSPDY+10){
	  ballSPDY=3;
	  }
	  if(ballY>400) {
		  ballX=250;
		  ballY=200;
		  ballSPDX=0;
		  ballSPDY=3;
		  R=0;
		  G=0;
		  B=0;
	  }
	  
	  if(ballX+ballSPDX<1||ballX+ballSPDX>499){
	    ballSPDX=-ballSPDX;
	  }
	    ballX+=ballSPDX;
	    ballY+=ballSPDY;
	    if(abs(ballX-250)<25&&abs(ballY-50)<25){
	    Random rand = new Random();
	    R = rand.nextInt(255);
	    G = rand.nextInt(255);
	    B = rand.nextInt(255);
	    }
	  rect(mouseX-50,380,100,20);
	  ellipse(ballX,ballY,20,20);
	  stroke(255, 255, 255);
	  fill(R, G, B);
	  ellipse(250,50,50,50);

	}
}