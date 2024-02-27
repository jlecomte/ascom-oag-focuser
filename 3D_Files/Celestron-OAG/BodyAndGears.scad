/*
 * Openscad design for case to use on Celestron OAG for Julien Lecomte's
 * ASCOM-Compatible OAG Focuser
 *
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */


 
 // https://github.com/chrisspen/gears
 // https://github.com/revarbat/BOSL

include <BOSL/constants.scad>
use <BOSL/shapes.scad>
include <gears-master/gears.scad>


$fn=300;
//$fn=20;


// Dimensions for Celestron Base

OAGWidth = 30.6;
OAGLength = 42.8+0.2;
OAGAngle = 45;
OAGOffset = (6.5-5.5)/2;  // The OAG helical is offset from the center of the OAG towards the OTA side
OAGClampAreaHeight = 3.9 - 0.4;
OAGHelicalRotatingShaftDiameter = 51.85;

// Dimensions for Celestron Gear
FudgeFactor = 0.1;
GearInnerDiameter = OAGHelicalRotatingShaftDiameter+FudgeFactor;
GearWidth = 6.15;
NumberOfTeeth = 60;
NumberOfPinionTeeth = 16;

PinionShaftDiameter =4.89;
PinionShaftNotchWidth = 2.9;
PinionShaftDepth = 5;


UpShiftToClearOAG = 5;
BaseExtension = 10;

BaseWidthA = 53.65;
BaseWidthB = 50.5;
BaseSection1Length = 90; // Gets cut off by the clamp hemicircle, just needs to be long enough 
BaseClampRingThickness = 6 + UpShiftToClearOAG;
BaseSection1ThicknessA = BaseClampRingThickness/2;
BaseSection1ThicknessB = BaseClampRingThickness/2;
VerticalLockingSlotLength = 10;
DiagonalSlotLength = sqrt(pow(BaseClampRingThickness/3,2) + pow(VerticalLockingSlotLength,2));
AngleForDiagonalSlot = atan(VerticalLockingSlotLength / (BaseClampRingThickness/3) );

BaseCoverBoltHoleXOffset = 4  +.5 + 1;
BaseCoverBoltHoleXSeparation = 47.5+ BaseExtension - 2;
BaseCoverBoltHoleYSeparation = 45 -1 -2;
BaseCoverInsertOuterDiameter = 7;


GapToHelicalRotatingShaft = 1.5; // How much space around rotating section of helix focus to allow for ring
CelestronClampingAreaTubeDiameter = 42.8;
BaseClampInnerDiameter = 42.8 + 2;
BaseClampWallThickness = BaseWidthA-BaseWidthB; // Doesn't need to be this
BaseClampOuterDiameter = OAGHelicalRotatingShaftDiameter + 2*GapToHelicalRotatingShaft + 2*BaseClampWallThickness;



BaseCornerFillet = 4;
CoverBoltHoleDiameter = 3.15;
CoverBoltInsertDiameter = 3.9;
CoverBoltHoleALength = 7;
CoverBoltHoleBLength = 5;
BoltHoleXAdjust = CoverBoltHoleDiameter*1.5;
CoverWallThickness = 2.5;
CoverWallHeight = 22.5-GearWidth +4.7;  // This is a fudge, manually adjusted
TopCoverThickness = 3;
TopCoverLipThickness = 2;

ClampBoltHoleDiameter = 3.2;
ClampGap = 1.5;
ClampBoltHeadDiameter = 5.4 + .5;
ClampNutWidth = 5.4 + 0.2;
ClampBoltNutHoleDepth = 16;

StepperHoleSpacing = 35;
StepperBoltSlotLength = 4.5 + 2;
StepperBoltSlotWidth = 3.15;
StepperBoltDiameter = 3.1;
SlotFillet = 1.5;
HalfSlotLengthPlay = (StepperBoltSlotLength-StepperBoltDiameter);
StepperShaftOffsetFromCenter=8;

StepperSlot1XOffset = 12;
StepperSlot1Width = 18;
StepperSlot1Length = 2;

StepperSlot2Width = 15;
StepperSlot2LengthTop = 2.7;
StepperSlot2SlopeAngle = 45;
StepperCenterOffsetFromRightEdge =  36.5+10 + BaseExtension;
StepperHoleDiameter = 28 + 1.5;


// Circuit Board Parameters
CircuitBoardHoleHeight = 2.71;
CircuitBoardHoleOuterDiameter = 5.2;
CircuitBoardHoleInnerDiameter = 3.2 + 0.2;
CircuitBoardHoleSeparationX = 37;  // Measured in Prusaslicer
CircuitBoardHoleSeparationY = 37.6;  // Measured in Prusaslicer
CircuitBoardHoleOffsetX = 10; // Offset from X=0 end
//CircuitBoardHoleOffsetY = 5; // Offset from Y=BaseWidthA/2 side;   DECIDED TO CENTER
CircuitBoardSinkIntoPlate = 4;
CircuitBoardUSBHoleXOffsetFromInsert = 23.8; //
CircuitBoardUSBHoleOffsetFromRightEdge = CircuitBoardHoleOffsetX + CircuitBoardUSBHoleXOffsetFromInsert;
CircuitBoardUSBZOffsetFromTopOfInsert = 7;
CircuitBoardUSBZDim = 7;
CircuitBoardUSBXDim = 15;



// Measured off printed parts, not derived and not quite accurate...
//SmallGearDiameter = 16;
SmallGearDiameter = 16 * floor(14*NumberOfTeeth/52)/14;
//LargeGearDiameter = 61.5;
//LargeGearDiameter = 81.5;
LargeGearDiameter = 61.5;
GearOverlap = 1.9;
GearCentersSeparation = (SmallGearDiameter + LargeGearDiameter)/2 - GearOverlap;
echo("GearCentersSeparation = ", GearCentersSeparation);


CelestronLargeSCTDiameter1 = 94.6;
CelestronLargeSCTDiameter2 = 89.2;
CelestronLargeSCTOffsetFromBottom =  11-4.4; // Measured
CelestronLargeSCTInsetFromSide = 15.08 - 11.45 + 1.5;
CelestronLargeSCTRingHeight1 = 11.5;
CelestronLargeSCTRingHeight2 = 2.5;







//rotate([180,0,0])
//CelestronBaseA();
//rotate([180,0,0])
//CelestronBaseB();
//CelestronGear();
//rotate([180,0,0])
translate([0,0,-GearWidth - 6 - TopCoverThickness -4.7])
TopCover();
//CelestronBase();
//CoverBoltHoleCutouts();
//CoverWallSection();
//MotorCutouts();
//DiagonalSlot();
//InsideSlice();

// MakeANut(10,5);
//OAGBlock();
//ClampBoltSection();

//translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation,0,-GearWidth - 6])
// CelestronGear();

//translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter,0,-GearWidth - 6])
//rotate([0,180,0])
// MotorPinion();

module TopCover() {
     XLengthA = 2*BaseCoverBoltHoleXOffset + BaseCoverBoltHoleXSeparation;
     XLengthB = XLengthA - (BaseWidthA-BaseWidthB);
     mirror([0,0,1]) {
	  difference() {
	       union() {
		    hull() { // Create beveled edge around plate
			 union() {
			      translate([XLengthA/2, 0, TopCoverThickness/4-BaseCornerFillet])
				   difference() {
				   cuboid([XLengthA, BaseWidthA, TopCoverThickness/2+BaseCornerFillet*2], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
				   translate([0,0,-TopCoverThickness/2/2-0.0001])
					cube([XLengthA+0.1, BaseWidthA+0.1, BaseCornerFillet*2], center=true);
			      }
			      translate([XLengthA/2, 0, TopCoverThickness/4-BaseCornerFillet+TopCoverThickness/2-0.000001])
				   difference() {
				   cuboid([XLengthB, BaseWidthB, TopCoverThickness/2+BaseCornerFillet*2], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
				   translate([0,0,-TopCoverThickness/2/2-0.0001])
					cube([XLengthB+0.1, BaseWidthB+0.1, BaseCornerFillet*2], center=true);         
			      }       

			 }
		    }
		    /* translate([XLengthA/2, 0, TopCoverThickness/4-BaseCornerFillet+TopCoverThickness/2-0.000001]) */
		    /* 	 difference() { */
		    /* 	 cuboid([XLengthB, BaseWidthB, TopCoverLipThickness+BaseCornerFillet*2], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL); */
		    /* 	 translate([0,0,-TopCoverThickness/2/2-0.0001]) */
		    /* 	      cuboid([XLengthB+0.1, BaseWidthB+0.1, BaseCornerFillet*2], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL); */
		    /* } */
		    XLength = 2*BaseCoverBoltHoleXOffset + BaseCoverBoltHoleXSeparation;
		    ZThickness = TopCoverLipThickness + 200;
		    translate([XLengthA/2,0,-TopCoverLipThickness])
			 difference() {
			 translate([0,0,ZThickness/2])
			      cuboid([XLength-2*CoverWallThickness -0.4 , BaseWidthA-2*CoverWallThickness -0.4 , ZThickness], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
			 translate([0,0,TopCoverLipThickness + ZThickness/2])
			      cuboid([XLength-2*CoverWallThickness+0.1, BaseWidthA-2*CoverWallThickness+0.1, ZThickness], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
		    }

	       }
	       union() {
		    translate([0,0,-6])
			 MotorCutouts();
		    CoverBoltHoleCutouts();
	       }
	  }
     }

}

module CelestronGear() {
     herringbone_gear(modul=1, tooth_number=NumberOfTeeth, width=GearWidth, bore=GearInnerDiameter, pressure_angle=20, helix_angle=20);
}

module MotorPinion() {
     difference() {
	  herringbone_gear(modul=1, tooth_number=NumberOfPinionTeeth, width=GearWidth, bore=0, pressure_angle=20, helix_angle=20);
	  translate([0,0,-0.001])
	  difference() {
	       cylinder(d=PinionShaftDiameter, h=PinionShaftDepth);
	   union() {
	       translate([PinionShaftNotchWidth/2 + 5,0,0])
		    cube([10,10,PinionShaftDepth], center=true);
	       translate([-PinionShaftNotchWidth/2 - 5,0,0])
		    cube([10,10,PinionShaftDepth], center=true);
	  }
     }
     }
}



module CircuitBoardSink() {
     echo("Circuit Board Sink Into Plate = ",CircuitBoardSinkIntoPlate);
     XLength = 2*BaseCoverBoltHoleXOffset + BaseCoverBoltHoleXSeparation - BaseExtension;
     translate([XLength/2, 0, CircuitBoardSinkIntoPlate - 0.01])
	  difference() {
	  translate([0,0,-CoverWallHeight/2])
	       cuboid([XLength-2*CoverWallThickness, BaseWidthA-2*CoverWallThickness, CoverWallHeight], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
	  translate([0,0,-CoverWallHeight/2 - CircuitBoardSinkIntoPlate/2 -0.01])
	       cuboid([XLength-2*CoverWallThickness+1, BaseWidthA-2*CoverWallThickness +1, CoverWallHeight-CircuitBoardSinkIntoPlate], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
     }
}


module CoverWallSection() {
     XLength = 2*BaseCoverBoltHoleXOffset + BaseCoverBoltHoleXSeparation;
     difference() {
	  translate([XLength/2, 0, -CoverWallHeight/2+0.01]) 
	       difference() {
	       cuboid([XLength, BaseWidthA, CoverWallHeight], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
	       cuboid([XLength-2*CoverWallThickness, BaseWidthA-2*CoverWallThickness, CoverWallHeight+0.1], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
	  }
	  translate([CircuitBoardUSBHoleOffsetFromRightEdge, BaseWidthA/2, CircuitBoardSinkIntoPlate - CircuitBoardHoleHeight - CircuitBoardUSBZOffsetFromTopOfInsert])
	       cuboid([CircuitBoardUSBXDim, 20, CircuitBoardUSBZDim], fillet=CircuitBoardUSBZDim/2, center=true, edges=EDGES_Y_ALL);
     }

     CoverBoltInserts();

}

module CelestronBase() {

     difference() {  // Here we cut out the slot for the clamp and drill the inner bolt holes for the inner clamp bolt and make a slot to give vertical locking to the 2 clamp sections

	  // Main Section
	  union() {
	       difference() {
		    // The filleted, beveled base plate
		    union() {
			 // Create top part of clamping ring
			 translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation,0,(BaseClampRingThickness)/2])
			      cyl(h=BaseClampRingThickness, d=BaseClampOuterDiameter, fillet2=BaseClampRingThickness/2, center=true);
			 translate([0,0,BaseClampRingThickness-(BaseSection1ThicknessA+BaseSection1ThicknessB)])
			      BasePlateWithBeveledEdge(BaseSection1Length);
		    }
		    union() { // Clamp cutouts for Helical focuser

			 // Clamp hole inner cutout
			 translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation,0,-100+UpShiftToClearOAG+OAGClampAreaHeight+0.001]) 
			      cylinder(h=100, d=BaseClampInnerDiameter, center=false);

			 translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation,OAGOffset,OAGClampAreaHeight+UpShiftToClearOAG])
			      OAGBlock();

			 translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation,0,UpShiftToClearOAG/2-0.001])
			      cylinder(h=UpShiftToClearOAG, d=GearInnerDiameter+GapToHelicalRotatingShaft*2, center=true);

		    }
	       }

	       // Outside Bolt Clamp
	       difference() {  
		    translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation+BaseClampOuterDiameter/2 - 4,0,BaseClampRingThickness/2])
			 ClampBoltSection();
		    translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation,0,UpShiftToClearOAG/2-0.001])
			 cylinder(h=UpShiftToClearOAG, d=GearInnerDiameter+GapToHelicalRotatingShaft*2, center=true);
	       }

	  }

	  union() {
	       translate([BaseClampOuterDiameter+StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2 - BoltHoleXAdjust ,0,0])
		    cube([2*BaseClampOuterDiameter, ClampGap, 500], center=true);

	       translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2 ,0,BaseClampRingThickness/2])
		    rotate([90, 0, 0])
		    cylinder(h=1000, d=ClampBoltHoleDiameter, center=true);

	       translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2 , -ClampBoltNutHoleDepth/2 + BaseWidthA/2 + 0.1 ,BaseClampRingThickness/2])
		    rotate([90, 0, 0])
		    cylinder(h=ClampBoltNutHoleDepth, d=ClampBoltHeadDiameter, center=true);

	       translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2 , ClampBoltNutHoleDepth/2 - BaseWidthA/2 - 0.1 ,BaseClampRingThickness/2])
		    translate([0,ClampBoltNutHoleDepth/2-0.01,0])
		    rotate([90, 0, 0])
		    rotate([0,0,30])
		    MakeANut(ClampNutWidth,ClampBoltNutHoleDepth);

	       // Diagonal slot
	       DiagonalSlot();

	       // Inside slice
	       InsideSlice();

	       // Circuit board sink into plate
	       CircuitBoardSink();

	       LargeSCTRing();

		    
	  }

     }

     translate([0,0,0.01])
	  difference() {
	  CoverWallSection();
	  union() {
	  }
	  translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter,0,-8+GearWidth/2 - 100/2])
	       cylinder(d=SmallGearDiameter + 2, h=100, center=true);
	  translate([StepperCenterOffsetFromRightEdge+HalfSlotLengthPlay,0,-12 - GearWidth/2 - 100/2])
	       cylinder(d=StepperHoleDiameter, h=100, center=true);
     }

     // Posts for circuit board inserts
     CircuitBoardInserts();

}

module DiagonalSlot() {
     translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2  - BoltHoleXAdjust ,0,BaseClampRingThickness+0.1])
	  rotate([0, AngleForDiagonalSlot, 0])
	  translate([0,0,-DiagonalSlotLength])
	  cube([0.2, BaseWidthA, DiagonalSlotLength+0.1]);
     translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2  - BoltHoleXAdjust ,0,-0.1])
	  rotate([0, -AngleForDiagonalSlot, 0])
	  translate([0,0,0])
	  cube([0.2, BaseWidthA, DiagonalSlotLength+0.1]);
     translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2  - BoltHoleXAdjust -VerticalLockingSlotLength ,0, BaseClampRingThickness/3])
	  cube([0.2, BaseWidthA, BaseClampRingThickness/3]);

}

module CelestronBaseA() {
     intersection() {
	  CelestronBase();
	  SeparationBlock();
     }
}

module CelestronBaseB() {
     difference() {
	  CelestronBase();
	  SeparationBlock();
     }
}

module SeparationBlock() {
     difference() {
	  union() {
	       hull() DiagonalSlot();
	       translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2  - BoltHoleXAdjust  ,0,-0.001])
		    cube([1000, BaseWidthA, 1000]);
	  }
	  translate([-0.15, 0, 0])
	       DiagonalSlot();
     }
}

module InsideSlice() {
     intersection() {
	  translate([0, -5, 0]) hull() DiagonalSlot();

	  translate([BaseClampOuterDiameter+StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation - BaseClampOuterDiameter/2 - BoltHoleXAdjust -VerticalLockingSlotLength ,0,0])
	       cube([2*BaseClampOuterDiameter, ClampGap, 500], center=true);
     }
}


module OAGBlock() {

     translate([0,0,5])
	  intersection() {
	  linear_extrude(height=10, center=true, scale = sqrt(2)*1.2)
	       square([OAGLength, OAGWidth], center=true);
	  cube([1000, OAGWidth, 1000], center=true);
     }



}


module ClampBoltSection() {

     translate([12/2-1.2,0,0])
	  difference() {
	  cuboid([12,10,BaseClampRingThickness], fillet=5, center=true, edges=EDGES_Y_RT);
//	  cuboid([11,10,BaseClampRingThickness], fillet=BaseClampRingThickness/2, center=true, edges=EDGES_Y_RT);
	  translate([2,0,0])
	       rotate([90,0,0])
	       cylinder(d=ClampBoltHoleDiameter, h=1000, center=true);

     }
}



module CoverBoltHoleCutouts() {
     union() { // Cover bolt holes
	  translate([BaseCoverBoltHoleXOffset, BaseCoverBoltHoleYSeparation/2, 0])
	       cylinder(h=1000, d=CoverBoltHoleDiameter, center=true);
        
	  translate([BaseCoverBoltHoleXOffset, -BaseCoverBoltHoleYSeparation/2, 0])
	       cylinder(h=1000, d=CoverBoltHoleDiameter, center=true);
               
	  translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, BaseCoverBoltHoleYSeparation/2, 0])
	       cylinder(h=1000, d=CoverBoltHoleDiameter, center=true);
        
	  translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, -BaseCoverBoltHoleYSeparation/2, 0])
	       cylinder(h=1000, d=CoverBoltHoleDiameter, center=true);
     }


}

module CoverBoltInserts() {
     translate([0,0,-CoverWallHeight + TopCoverLipThickness+1+2]) {
	  translate([0,0,CoverBoltHoleALength/2]) {
	       difference() {
		    union() { // Cover bolt Inserts A section
			 translate([BaseCoverBoltHoleXOffset, BaseCoverBoltHoleYSeparation/2, -2])
			      cylinder(h=CoverBoltHoleALength, d=BaseCoverInsertOuterDiameter, center=true);
			 translate([BaseCoverBoltHoleXOffset, -BaseCoverBoltHoleYSeparation/2, -2])
			      cylinder(h=CoverBoltHoleALength, d=BaseCoverInsertOuterDiameter, center=true);
			 translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, BaseCoverBoltHoleYSeparation/2, 0])
			      cylinder(h=CoverBoltHoleALength, d=BaseCoverInsertOuterDiameter, center=true);
			 translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, -BaseCoverBoltHoleYSeparation/2, 0])
			      cylinder(h=CoverBoltHoleALength, d=BaseCoverInsertOuterDiameter, center=true);
		    }
		    union() { // Cover bolt Insert Cutouts
			 translate([BaseCoverBoltHoleXOffset, BaseCoverBoltHoleYSeparation/2, -2])
			      cylinder(h=1000, d=CoverBoltInsertDiameter, center=true);
			 translate([BaseCoverBoltHoleXOffset, -BaseCoverBoltHoleYSeparation/2, -2])
			      cylinder(h=1000, d=CoverBoltInsertDiameter, center=true);
			 translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, BaseCoverBoltHoleYSeparation/2, 0])
			      cylinder(h=1000, d=CoverBoltInsertDiameter, center=true);
			 translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, -BaseCoverBoltHoleYSeparation/2, 0])
			      cylinder(h=1000, d=CoverBoltInsertDiameter, center=true);
		    }
	       }
	  }
	  translate([0,0,CoverBoltHoleALength + CoverBoltHoleBLength/2]) {
     	       union() { // Cover bolt Inserts A section
		    translate([BaseCoverBoltHoleXOffset, BaseCoverBoltHoleYSeparation/2, -2])
			 SlicedBoltInsertCylinder(RotationAngle = -45);
		    translate([BaseCoverBoltHoleXOffset, -BaseCoverBoltHoleYSeparation/2, -2])
			 SlicedBoltInsertCylinder(RotationAngle = 45);
		    translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, BaseCoverBoltHoleYSeparation/2, 0])
			 SlicedBoltInsertCylinder(RotationAngle = 225);
		    translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, -BaseCoverBoltHoleYSeparation/2, 0])
			 SlicedBoltInsertCylinder(RotationAngle = -225);
		    /* translate([BaseCoverBoltHoleXOffset, BaseCoverBoltHoleYSeparation/2, 0]) */
		    /* 	 cylinder(h=CoverBoltHoleBLength, d1=BaseCoverBoltHoleXOffset*2, d2=0, center=true); */
		    /* translate([BaseCoverBoltHoleXOffset, -BaseCoverBoltHoleYSeparation/2, 0]) */
		    /* 	 cylinder(h=CoverBoltHoleBLength, d1=BaseCoverBoltHoleXOffset*2, d2=0, center=true); */
		    /* translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, BaseCoverBoltHoleYSeparation/2, 0]) */
		    /* 	 cylinder(h=CoverBoltHoleBLength, d1=BaseCoverBoltHoleXOffset*2, d2=0, center=true); */
		    /* translate([BaseCoverBoltHoleXOffset+BaseCoverBoltHoleXSeparation, -BaseCoverBoltHoleYSeparation/2, 0]) */
		    /* 	 cylinder(h=CoverBoltHoleBLength, d1=BaseCoverBoltHoleXOffset*2, d2=0, center=true); */
	       }
	  }
     }

}

//SlicedBoltInsertCylinder();
     
module SlicedBoltInsertCylinder(RotationAngle = 45) {
     angle =atan(CoverBoltHoleBLength/BaseCoverInsertOuterDiameter);
     rotate([0,0,RotationAngle])
	  difference() {
	  cylinder(h=CoverBoltHoleBLength, d=BaseCoverInsertOuterDiameter, center=true);
	  rotate([0,angle,0])
	       translate([0,0,250])
	       cube([BaseCoverBoltHoleXOffset*2 *2, BaseCoverBoltHoleXOffset*2 *2, 500], center=true);
     }

}


module CircuitBoardInserts() {
     translate([0,0,CircuitBoardSinkIntoPlate+0.01]) {
	  translate([0,0,-CircuitBoardHoleHeight/2]) {
	       difference() {
		    union() { // Cover bolt Inserts A section
			 translate([CircuitBoardHoleOffsetX, CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=CircuitBoardHoleHeight, d=CircuitBoardHoleOuterDiameter, center=true);
			 translate([CircuitBoardHoleOffsetX, -CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=CircuitBoardHoleHeight, d=CircuitBoardHoleOuterDiameter, center=true);
			 translate([CircuitBoardHoleOffsetX+CircuitBoardHoleSeparationX, CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=CircuitBoardHoleHeight, d=CircuitBoardHoleOuterDiameter, center=true);
			 translate([CircuitBoardHoleOffsetX+CircuitBoardHoleSeparationX, -CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=CircuitBoardHoleHeight, d=CircuitBoardHoleOuterDiameter, center=true);
		    }
		    union() { // Cover bolt Insert Cutouts
			 translate([CircuitBoardHoleOffsetX, CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=1000, d=CircuitBoardHoleInnerDiameter, center=true);
			 translate([CircuitBoardHoleOffsetX, -CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=1000, d=CircuitBoardHoleInnerDiameter, center=true);
			 translate([CircuitBoardHoleOffsetX+CircuitBoardHoleSeparationX, CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=1000, d=CircuitBoardHoleInnerDiameter, center=true);
			 translate([CircuitBoardHoleOffsetX+CircuitBoardHoleSeparationX, -CircuitBoardHoleSeparationY/2, 0])
			      cylinder(h=1000, d=CircuitBoardHoleInnerDiameter, center=true);
		    }
	       }
	  }
     }

}

module MotorCutouts() {
     hull()  { // Motor Stretched Circular cutout
	  union() {
	       translate([StepperCenterOffsetFromRightEdge+HalfSlotLengthPlay,0,0])
		    cylinder(h=1000, d=StepperHoleDiameter, center=true);
		    
	       translate([StepperCenterOffsetFromRightEdge-HalfSlotLengthPlay,0,0])
		    cylinder(h=1000, d=StepperHoleDiameter, center=true);
		    
	  }
     }

     union() { // Motor bolt hole slots
	  translate([StepperCenterOffsetFromRightEdge,-StepperHoleSpacing/2,0])
	       cuboid([StepperBoltSlotLength, StepperBoltSlotWidth, 1000], fillet=SlotFillet, edges=EDGES_Z_ALL, center=true);
	  translate([StepperCenterOffsetFromRightEdge,StepperHoleSpacing/2,0])
	       cuboid([StepperBoltSlotLength, StepperBoltSlotWidth, 1000], fillet=SlotFillet, edges=EDGES_Z_ALL, center=true);
	       
        
     }

     // Motor Wire Housing cutouts
     union() {

	  translate([StepperCenterOffsetFromRightEdge-StepperSlot1Length/2 - StepperSlot1XOffset - HalfSlotLengthPlay,0,0])
	       cube([StepperSlot1Length, StepperSlot1Width, 1000], center=true);

	  translate([StepperCenterOffsetFromRightEdge-StepperSlot1Length/2 - StepperSlot1XOffset - HalfSlotLengthPlay - StepperSlot1Length,0,0])
	       cube([StepperSlot2LengthTop, StepperSlot2Width, 1000], center=true);

	  Offset = StepperCenterOffsetFromRightEdge-StepperSlot1Length/2 - StepperSlot1XOffset - HalfSlotLengthPlay - StepperSlot1Length;
	  translate([-(BaseSection1ThicknessA+BaseSection1ThicknessB)/tan(90-StepperSlot2SlopeAngle) + Offset ,0,0])
	       rotate([0, StepperSlot2SlopeAngle, 0])
	       translate([(BaseSection1ThicknessA+BaseSection1ThicknessB)*2,0,(BaseSection1ThicknessA+BaseSection1ThicknessB)])
	       cube([(BaseSection1ThicknessA+BaseSection1ThicknessB)*4, StepperSlot2Width, (BaseSection1ThicknessA+BaseSection1ThicknessB)*2+10], center=true);


     }


}

module BasePlateWithBeveledEdge(L) {

     L_B = L - (BaseWidthA-BaseWidthB);

     hull() { // Create beveled edge around plate
	  union() {
	       translate([L/2, 0, BaseSection1ThicknessA/2-BaseCornerFillet])
		    difference() {
		    cuboid([L, BaseWidthA, BaseSection1ThicknessA+BaseCornerFillet*2], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
		    translate([0,0,-BaseSection1ThicknessA/2-0.0001])
			 cube([L+0.1, BaseWidthA+0.1, BaseCornerFillet*2], center=true);
	       }
	       translate([L/2, 0, BaseSection1ThicknessB/2-BaseCornerFillet+BaseSection1ThicknessA-0.000001])
		    difference() {
		    cuboid([L_B, BaseWidthB, BaseSection1ThicknessB+BaseCornerFillet*2], center=true, fillet=BaseCornerFillet, edges = EDGES_Z_ALL);
		    translate([0,0,-BaseSection1ThicknessA/2-0.0001])
			 cube([L_B+0.1, BaseWidthB+0.1, BaseCornerFillet*2], center=true);         
	       }       

	  }
     }

}



module MakeANut(m,h) {
     linear_extrude(height = h, twist=0) {
	  MakeAHexagon(m);
     }
}
module MakeAHexagon(m) {
    
     a=m/sqrt(3);
     b=sqrt(3)*a/2;
    
     points=[
	  [a,0], [a/2, b], [-a/2, b], [-a, 0], [-a/2, -b], [a/2, -b]];
    
     polygon(points);
}

module LargeSCTRing() {

//     translate([0,-BaseClampOuterDiameter + CelestronLargeSCTDiameter1/2 -CelestronLargeSCTInsetFromSide - (CelestronLargeSCTRingHeight1+ CelestronLargeSCTRingHeight2)])
     translate([StepperCenterOffsetFromRightEdge+StepperShaftOffsetFromCenter + GearCentersSeparation, -BaseClampOuterDiameter/2 - (CelestronLargeSCTRingHeight1+ CelestronLargeSCTRingHeight2) + CelestronLargeSCTInsetFromSide, CelestronLargeSCTDiameter1/2 - CelestronLargeSCTOffsetFromBottom + BaseClampRingThickness])
	  rotate([90,0,0])
	  hull() {
	  translate([0,0,-CelestronLargeSCTRingHeight1/2]) 
	       cylinder(h=CelestronLargeSCTRingHeight1, d=CelestronLargeSCTDiameter1, center=true);
	  translate([0,0,-CelestronLargeSCTRingHeight2/2 - CelestronLargeSCTRingHeight1]) 
	       cylinder(h=CelestronLargeSCTRingHeight2, d=CelestronLargeSCTDiameter2, center=true);
     }


}


