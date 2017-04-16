import { TwinPairs.ClientPage } from './app.po';

describe('twin-pairs.client App', () => {
  let page: TwinPairs.ClientPage;

  beforeEach(() => {
    page = new TwinPairs.ClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
