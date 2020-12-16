###################################################
### chunk number 1: 
###################################################
#line 106 "Sweave.Rnw"
rnwfile <- system.file("Sweave", "example-1.Rnw", package = "utils")
Sweave(rnwfile)


###################################################
### chunk number 2: 
###################################################
#line 113 "Sweave.Rnw"
tools::texi2dvi("example-1.tex", pdf = TRUE)


###################################################
### chunk number 3: 
###################################################
#line 348 "Sweave.Rnw"
SweaveSyntConv(rnwfile, SweaveSyntaxLatex)


###################################################
### chunk number 4:  eval=FALSE
###################################################
## #line 446 "Sweave.Rnw"
## help("Sweave")


###################################################
### chunk number 5:  eval=FALSE
###################################################
## #line 456 "Sweave.Rnw"
## help("RweaveLatex")


###################################################
### chunk number 6:  eval=FALSE
###################################################
## #line 564 "Sweave.Rnw"
## help("Rtangle")


