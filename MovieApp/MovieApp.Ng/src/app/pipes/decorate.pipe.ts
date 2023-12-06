import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'decorate',
  standalone: true
})
export class DecoratePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    let stringVal = value as string;
    let pre = args[0] as string ?? "";
    let post = args[1] as string ?? "";
    return pre + stringVal + post;
  }

}
