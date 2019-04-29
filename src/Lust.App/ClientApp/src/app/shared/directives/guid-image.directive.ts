import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { ImageService, ImageProportions, ImageSizes } from 'app/core/services/image.service';




@Directive({
    selector: `[guidImage],
[guidImage.thumbnail],
[guidImage.xs],
[guidImage.sm],
[guidImage.md],
[guidImage.lg],
[guidImage.xl],
[guidImage.xxl],
[guidImage.thumbnail.1x1],
[guidImage.xs.1x1],
[guidImage.sm.1x1],
[guidImage.md.1x1],
[guidImage.lg.1x1],
[guidImage.xl.1x1],
[guidImage.xxl.1x1],
[guidImage.thumbnail.3x4],
[guidImage.xs.3x4],
[guidImage.sm.3x4],
[guidImage.md.3x4],
[guidImage.lg.3x4],
[guidImage.xl.3x4],
[guidImage.xxl.3x4],
[guidImage.thumbnail.4x3],
[guidImage.xs.4x3],
[guidImage.sm.4x3],
[guidImage.md.4x3],
[guidImage.lg.4x3],
[guidImage.xl.4x3],
[guidImage.xxl.4x3],
[guidImage.thumbnail.16x9],
[guidImage.xs.16x9],
[guidImage.sm.16x9],
[guidImage.md.16x9],
[guidImage.lg.16x9],
[guidImage.xl.16x9],
[guidImage.xxl.16x9],
[guidImage.thumbnail.9x16],
[guidImage.xs.9x16],
[guidImage.sm.9x16],
[guidImage.md.9x16],
[guidImage.lg.9x16],
[guidImage.xl.9x16],
[guidImage.xxl.9x16]
`
})

//p1x1,
//P3x4 ,
//p4x3,
//p16x9,
//p9x16

//thumbnail = 160,
//xs = 360,
//sm = 640,
//md = 800,
//lg = 1280,
//xl = 1600,
//xxl = 1920




export class GuidImageDirective implements OnInit {


    private imageProportion: ImageProportions = 'original';
    private imageSize: ImageSizes = 'original';
    private id: string;
    private refreshEveryInit = false;

    /* tslint: disable */
    @Input('guidImage') set image(val) {
        this.id = val;
        this.update();
    };    

    @Input('guidImage.thumbnail') set imagethumbnail(val) {
        this.id = val;
        this.imageSize = 'thumbnail';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.xs') set imagexs(val) {
        this.id = val;
        this.imageSize = 'xs';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.sm') set imagesm(val) {
        this.id = val;
        this.imageSize = 'sm';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.md') set imagemd(val) {
        this.id = val;
        this.imageSize = 'md';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.lg') set imagelg(val) {
        this.id = val;
        this.imageSize = 'lg';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.xl') set imagexl(val) {
        this.id = val;
        this.imageSize = 'xl';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.xxl') set imagexxl(val) {
        this.id = val;
        this.imageSize = 'xxl';
        this.imageProportion = 'original';
        this.update();
    };
    @Input('guidImage.thumbnail.1x1') set imagethumbnail1x1(val) {
        this.id = val;
        this.imageSize = 'thumbnail';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.xs.1x1') set imagexs1x1(val) {
        this.id = val;
        this.imageSize = 'xs';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.sm.1x1') set imagesm1x1(val) {
        this.id = val;
        this.imageSize = 'sm';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.md.1x1') set imagemd1x1(val) {
        this.id = val;
        this.imageSize = 'md';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.lg.1x1') set imagelg1x1(val) {
        this.id = val;
        this.imageSize = 'lg';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.xl.1x1') set imagexl1x1(val) {
        this.id = val;
        this.imageSize = 'xl';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.xxl.1x1') set imagexxl1x1(val) {
        this.id = val;
        this.imageSize = 'xxl';
        this.imageProportion = 'p1x1';
        this.update();
    };
    @Input('guidImage.thumbnail.3x4') set imagethumbnail3x4(val) {
        this.id = val;
        this.imageSize = 'thumbnail';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.xs.3x4') set imagexs3x4(val) {
        this.id = val;
        this.imageSize = 'xs';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.sm.3x4') set imagesm3x4(val) {
        this.id = val;
        this.imageSize = 'sm';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.md.3x4') set imagemd3x4(val) {
        this.id = val;
        this.imageSize = 'md';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.lg.3x4') set imagelg3x4(val) {
        this.id = val;
        this.imageSize = 'lg';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.xl.3x4') set imagexl3x4(val) {
        this.id = val;
        this.imageSize = 'xl';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.xxl.3x4') set imagexxl3x4(val) {
        this.id = val;
        this.imageSize = 'xxl';
        this.imageProportion = 'P3x4';
        this.update();
    };
    @Input('guidImage.thumbnail.4x3') set imagethumbnail4x3(val) {
        this.id = val;
        this.imageSize = 'thumbnail';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.xs.4x3') set imagexs4x3(val) {
        this.id = val;
        this.imageSize = 'xs';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.sm.4x3') set imagesm4x3(val) {
        this.id = val;
        this.imageSize = 'sm';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.md.4x3') set imagemd4x3(val) {
        this.id = val;
        this.imageSize = 'md';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.lg.4x3') set imagelg4x3(val) {
        this.id = val;
        this.imageSize = 'lg';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.xl.4x3') set imagexl4x3(val) {
        this.id = val;
        this.imageSize = 'xl';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.xxl.4x3') set imagexxl4x3(val) {
        this.id = val;
        this.imageSize = 'xxl';
        this.imageProportion = 'p4x3';
        this.update();
    };
    @Input('guidImage.thumbnail.16x9') set imagethumbnail16x9(val) {
        this.id = val;
        this.imageSize = 'thumbnail';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.xs.16x9') set imagexs16x9(val) {
        this.id = val;
        this.imageSize = 'xs';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.sm.16x9') set imagesm16x9(val) {
        this.id = val;
        this.imageSize = 'sm';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.md.16x9') set imagemd16x9(val) {
        this.id = val;
        this.imageSize = 'md';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.lg.16x9') set imagelg16x9(val) {
        this.id = val;
        this.imageSize = 'lg';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.xl.16x9') set imagexl16x9(val) {
        this.id = val;
        this.imageSize = 'xl';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.xxl.16x9') set imagexxl16x9(val) {
        this.id = val;
        this.imageSize = 'xxl';
        this.imageProportion = 'p16x9';
        this.update();
    };
    @Input('guidImage.thumbnail.9x16') set imagethumbnail9x16(val) {
        this.id = val;
        this.imageSize = 'thumbnail';
        this.imageProportion = 'p9x16';
        this.update();
    };
    @Input('guidImage.xs.9x16') set imagexs9x16(val) {
        this.id = val;
        this.imageSize = 'xs';
        this.imageProportion = 'p9x16';
        this.update();
    };
    @Input('guidImage.sm.9x16') set imagesm9x16(val) {
        this.id = val;
        this.imageSize = 'sm';
        this.imageProportion = 'p9x16';
        this.update();
    };
    @Input('guidImage.md.9x16') set imagemd9x16(val) {
        this.id = val;
        this.imageSize = 'md';
        this.imageProportion = 'p9x16';
        this.update();
    };
    @Input('guidImage.lg.9x16') set imagelg9x16(val) {
        this.id = val;
        this.imageSize = 'lg';
        this.imageProportion = 'p9x16';
        this.update();
    };
    @Input('guidImage.xl.9x16') set imagexl9x16(val) {
        this.id = val;
        this.imageSize = 'xl';
        this.imageProportion = 'p9x16';
        this.update();
    };
    @Input('guidImage.xxl.9x16') set imagexxl9x16(val) {
        this.id = val;
        this.imageSize = 'xxl';
        this.imageProportion = 'p9x16';
        this.update();
    };







    @Input('refreshEveryInit') set setRefreshEveryInit(val) {
        this.refreshEveryInit = true;
    };

    @Input() defaultImage: string;

    /* tslint:enable */

    //proportion/size



    //, 
    constructor(private el: ElementRef, private imageService: ImageService) {
        
    }

    private iniciado = false;

  ngOnInit(): void {

    if (!this.defaultImage)
      this.defaultImage != '';

    if (this.defaultImage && this.defaultImage != '')
            this.el.nativeElement.src = this.defaultImage;

        this.iniciado = true;
        this.update();
    }

    private update() {
        if (this.iniciado && this.id && this.id !== '') {
            this.imageService.get(this.id, this.imageProportion, this.imageSize, this.refreshEveryInit)
                .subscribe(
                result => {
                    this.el.nativeElement.src = result;
                },
                fail => {
                    if (this.defaultImage && this.defaultImage != '')
                        this.el.nativeElement.src = this.defaultImage;
                    else
                        this.el.nativeElement.src = '';
                }
                )

        }

    }

}
