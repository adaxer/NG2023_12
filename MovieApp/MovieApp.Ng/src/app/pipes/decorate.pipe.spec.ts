import { DecoratePipe } from './decorate.pipe';

describe('DecoratePipe', () => {
  it('works with pre and postfix', () => {
    const pipe = new DecoratePipe();
    let result = pipe.transform("test", "<", ">");
    expect(result).toEqual("<test>");
  });

  it('works with just pre', () => {
    const pipe = new DecoratePipe();
    let result = pipe.transform("test", "<");
    expect(result).toEqual("<test");
  });
  
  it('works without params', () => {
    const pipe = new DecoratePipe();
    let result = pipe.transform("test");
    expect(result).toEqual("test");
  });
});
