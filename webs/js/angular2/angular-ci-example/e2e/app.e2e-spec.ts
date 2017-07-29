import { AngularCiExamplePage } from './app.po';

describe('angular-ci-example App', () => {
  let page: AngularCiExamplePage;

  beforeEach(() => {
    page = new AngularCiExamplePage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
