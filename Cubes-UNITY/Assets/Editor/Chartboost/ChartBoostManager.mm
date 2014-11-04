//
//  ChartboostManager.m
//  CB
//
//  Created by Mike DeSaro on 12/20/11.
//

#import "ChartboostManager.h"


void UnitySendMessage( const char * className, const char * methodName, const char * param );

void UnityPause( bool pause );


@implementation ChartboostManager

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

+ (ChartboostManager*)sharedManager
{
	static ChartboostManager *sharedSingleton;
	
	if( !sharedSingleton )
		sharedSingleton = [[ChartboostManager alloc] init];
	
	return sharedSingleton;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Private

+ (id)objectFromJson:(NSString*)json
{
	NSError *error = nil;
	NSData *data = [NSData dataWithBytes:json.UTF8String length:json.length];
    NSObject *object = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&error];
	
	if( error )
		NSLog( @"failed to deserialize JSON: %@ with error: %@", json, [error localizedDescription] );

    return object;
}


+ (NSString*)jsonFromObject:(id)object
{
	NSError *error = nil;
	NSData *jsonData = [NSJSONSerialization dataWithJSONObject:object options:0 error:&error];
	
	if( jsonData && !error )
		return [[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] autorelease];
	else
		NSLog( @"jsonData was null, error: %@", [error localizedDescription] );

    return @"{}";
}


+ (void)logError:(CBLoadError)error
{
	switch( error )
	{
		case CBLoadErrorInternal:
			NSLog( @"CBLoadErrorInternal" );
			break;
		case CBLoadErrorInternetUnavailable:
			NSLog( @"CBLoadErrorInternetUnavailable" );
			break;
		case CBLoadErrorTooManyConnections:
			NSLog( @"CBLoadErrorTooManyConnections: Too many requests are pending for that location" );
			break;
		case CBLoadErrorWrongOrientation:
			NSLog( @"CBLoadErrorWrongOrientation: Interstitial loaded with wrong orientation" );
			break;
		case CBLoadErrorFirstSessionInterstitialsDisabled:
			NSLog( @"CBLoadErrorFirstSessionInterstitialsDisabled: Interstitial disabled, first session" );
			break;
		case CBLoadErrorNetworkFailure:
			NSLog( @"CBLoadErrorNetworkFailure: Network request failed" );
			break;
		case CBLoadErrorNoAdFound:
			NSLog( @"CBLoadErrorNoAdFound: No ad received" );
			break;
		case CBLoadErrorSessionNotStarted:
			NSLog( @"CBLoadErrorSessionNotStarted: Session not started, use startSession method" );
			break;
		case CBLoadErrorUserCancellation:
			NSLog( @"CBLoadErrorUserCancellation" );
			break;
	}
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark ChartboostDelegate

// Interstitial
- (void)didFailToLoadInterstitial:(CBLocation)location withError:(CBLoadError)error
{
	UnityPause( false );

    UnitySendMessage( "ChartboostManager", "didFailToLoadInterstitial", location.UTF8String );

	NSLog( @"didFailToLoadInterstitial" );
	[ChartboostManager logError:error];
}


- (void)didCacheInterstitial:(CBLocation)location
{
	UnitySendMessage( "ChartboostManager", "didCacheInterstitial", location.UTF8String );
}


- (void)didDismissInterstitial:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didDismissInterstitial", location.UTF8String );
}


- (void)didCloseInterstitial:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didCloseInterstitial", location.UTF8String );
}


- (void)didClickInterstitial:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didClickInterstitial", location.UTF8String );
}


// More Apps
- (void)didFailToLoadMoreApps:(CBLocation)location withError:(CBLoadError)error
{
	UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didFailToLoadMoreApps", location.UTF8String );

	NSLog( @"didFailToLoadMoreApps" );
	[ChartboostManager logError:error];
}


- (void)didCacheMoreApps:(CBLocation)location
{
	UnitySendMessage( "ChartboostManager", "didCacheMoreApps", location.UTF8String );
}


- (BOOL)shouldDisplayMoreApps:(CBLocation)location
{
    UnityPause( true );
    return YES;
}


- (void)didDismissMoreApps:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didDismissMoreApps", location.UTF8String );
}


- (void)didCloseMoreApps:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didCloseMoreApps", location.UTF8String );
}


- (void)didClickMoreApps:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didClickMoreApps", location.UTF8String );
}


// Rewarded Video
- (void)didFailToLoadRewardedVideo:(CBLocation)location withError:(CBLoadError)error
{
	UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didFailToLoadRewardedVideo", location.UTF8String );

	NSLog( @"didFailToLoadRewardedVideo" );
	[ChartboostManager logError:error];
}


- (void)didCacheRewardedVideo:(CBLocation)location
{
	UnitySendMessage( "ChartboostManager", "didCacheRewardedVideo", location.UTF8String );
}


- (BOOL)shouldDisplayRewardedVideo:(CBLocation)location
{
	UnityPause( true );
	return YES;
}


- (void)didDismissRewardedVideo:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didDismissRewardedVideo", location.UTF8String );
}


- (void)didCloseRewardedVideo:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didCloseRewardedVideo", location.UTF8String );
}


- (void)didClickRewardedVideo:(CBLocation)location
{
    UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didClickRewardedVideo", location.UTF8String );
}


- (void)didCompleteRewardedVideo:(CBLocation)location withReward:(int)reward
{
	UnityPause( false );
    UnitySendMessage( "ChartboostManager", "didCompleteRewardedVideo", [NSString stringWithFormat:@"%i", reward].UTF8String );
}


@end
