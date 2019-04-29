import { Pipe, PipeTransform } from '@angular/core';
import { ImageSizes, ImageProportions, ImageService } from 'app/core/services/image.service';

@Pipe({
  name: 'guidImage'
})
export class GuidImagePipe implements PipeTransform {

  constructor( private imageService: ImageService) {

  }

  transform(id: string, imageSize: ImageSizes, imageProportion?: ImageProportions): any {
    imageSize = imageSize == null ? 'original' : imageSize;
    imageProportion = imageProportion == null ? 'original' : imageProportion;

    return this.imageService.get(id, imageProportion, imageSize, false)
      

  }

}
