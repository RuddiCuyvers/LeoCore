/* JQuery mapwidget

 author: Anne Blankert, Geodan Systems & Research, Amsterdam, june 2010

 Some basic ideas for this plugin:
 - OpenLayers is a fully functional map library. It's functions should be (re-) used as much as possible
 - The JQuery libray allows developers to quickly create highly interactive web applications across multiple browser platforms
 - This plugin, 'mapwidget', aims to combine both of these worlds
 
 The main idea behind mapwidget is to be able to transform webpages from merely showing a single (though fully functional) 
 map into full featured web applications. Instead of having all controls inside the Openlayers map, the mapwidget plugin 
 allows developers to put these controls anywhere on the webpage and allow jQuery to take control of their look and feel.
 
 The visual styles applied by the plugin should relate only to functionality (collapse, slide, hide, unhide, ..). 
 Other style settings (fonts, colors, backgrounds, ..) should be applied by the webpage designer.
 
 Some of these map controls are:
 - an external overview map, to be incorporated in for instance a jquery.dialog()
 - an external scale bar, an external geographic mouse coordinate tracker
 - an external legend/layer manager for selecting base layers, managing layer order, layer transparency, displaying layer legends etc.
 - an external layer catalogue widget that allows you the select layers to be added to the map from among a large catalogue of layers, 
   possibly even including the possibility to add layers to the catalogue from mapserver capabilities and CSW catalogues
 
 Of course, developers should be able to combine the resulting map application with other jquery libraries and plugins, such as jquery.ui.layout.
 
 Mapwidget API basics:
 This plugin adds a new wrapper method 'mapwidget()' to JQuery.
 The mapwidget() method generally expects three parameters:
 1. An OpenLayers Map. This is the map that the widget will relate to
 2. A mapwidget type or function selector (string)
 3. An optional Javascript options object
 
 The plugin applies the selected function on the currently wrapped jQuery set which will usually result in some map information being
 displayed and possibly some map interactions being added to the DOM elements enclosed in the wrapped set. 
 
*/

(function ($) { // internally map "$" to "jQuery"

// extend jquery with mapwidget
$.fn.mapwidget = function (map, widgettype, options)
{
	if (!map)
	{
		alert ('mapwidget: map not passed');
		return this;
	}
	if (!widgettype)
	{
		alert ('mapwidget: widgettype not passed');
		return this;
	}
	switch (widgettype) // this is becoming quite a large switch statement...
	{
		case 'mouseposition':
			settings = $.extend ({ 
				labelLatitude: "latitude ",
				labelLongitude: "longitude ",
				decimalPlaces: 3,
				latLonSeparator: ", "
				}, options || {});
				
			// insert labels for longitude and latitude if not available
			$.each(this, function (index, element)
			{
				if ($('label#longitude', element).length == 0)
				{
					$(element).append(settings.labelLongitude + '<label id="longitude"> 0</label>');
				}
				if ($('label#latitude', element).length == 0)
				{
					$(element).append(settings.latLonSeparator + settings.labelLatitude + '<label id="latitude"> 0</label>');
				}
			});
			// register mousemove events
			latlongproj = new OpenLayers.Projection("EPSG:4326");
			//mercatorproj = new OpenLayers.Projection("EPSG:900913");
			var decimalPlaces = settings.decimalPlaces;
			map.events.register("mousemove", { jqwrapper: this, map: map }, function(e) {
				var mouseposition = this.map.events.getMousePosition(e);
				latlon = map.getLonLatFromPixel(mouseposition).transform(map.getProjectionObject(), latlongproj);
				$.each (this.jqwrapper, function (index, element) {
						$("#longitude", element).html((latlon.lon).toFixed(decimalPlaces));		
						$("#latitude", element).html((latlon.lat).toFixed(decimalPlaces));
					}
				);
			})
			break;
		case 'overviewmap':
			$.each (this, function (index, element)
				{
					// get id of this element
					id = $(element).attr("id");
					if (!id) {
						// generate unique id
						classname = $(element).attr("class");
						if (!classname) classname = "";
						id = classname.replace(" ", "-") + '-mapwidget-overviewmap-' + index;
						$(element).attr("id", id);
					}
					var overviewmapsize = { w: $(element).width(), h: $(element).height() };
					var overview = new OpenLayers.Control.OverviewMap( {div: document.getElementById(id), size: overviewmapsize} );
					map.addControl(overview);
					// remove OpenLayers blue border around overviewmap
					$('.olControlOverviewMapElement', element).removeClass ('olControlOverviewMapElement');
				}
			);
			break;
		case 'scalebar':
			settings = $.extend ({ 
				labelScale: " scale: ",
				maxWidth: 200
			}, options || {});
			this.data ("sbarsettings", settings); // store settings with scalebar
						
			_drawScaleBar (this, map);
			
			map.events.register("zoomend", {scalebar: this, map: map}, function(e) {
				//alert (map.getResolution());
				_drawScaleBar (this.scalebar, this.map);
			});
			break;
		case 'layercatalogue':
			settings = $.extend ({
				layerDefs: [{ title: map.baseLayer.name,					
									type: "baselayer",
									groupselector: "#baselayer",
									legendurl: ""
								}]
			}, options || {});
			
			layerDefs = settings.layerDefs;
			
			// do with all elements in the wrapped set..
			$.each(this, function (index, element) {
					// check if <ul><li>....</ul> exists in the element, otherwise insert it
					if ($("ul", element).length == 0)
					{
						$(element).append ('<ul></ul>');
					}
					// wrap <a> around text in <li> elements
					$.each($("li", element), function (index, liElement) {
							if (element.childNodes[0] && liElement.childNodes[0].nodeType == 3 /* node_text */) {
								$(liElement.childNodes[0]).wrap("<a></a>");
							}
						}
					);
				
					// create collapsible folder headers
					$("li a", element).addClass("folder").before('<span class="tocBtn">►&nbsp;</span>');
					$("li:not(:has(ul))", element).append ('<ul></ul>');
					
					// now attach layerdefs contents to the layerCatalogue widget
					$.each(layerDefs, function (index, layermetadata) {	
							$layerlink = $('<a class="layer-selector" id="layer'+ index + '">' + layermetadata.title + '</a>');
							if (!layermetadata.groupselector) layermetadata.groupselector = "";
							// store layer metadata with catalogue tree item
							$layerlink.data("layermetadata", layermetadata);
							if (layermetadata.groupselector != "") {
								// check if there are list elements in this element identified by the groupselector
								$listelements = $(layermetadata.groupselector + " ul", element);
								if ($listelements.length)
								{
									$listelements.append($('<li></li>').append($layerlink));
								} else {
									// groupselector not found, append to root of list
									$("ul:first", element).append($('<li></li>').append($layerlink));
								}
							} else {
								// otherwise append to root of list
								$("ul:first", element).append($('<li></li>').append($layerlink));
							}
						}
					);
					// put a layer icon or character before element in catalogue tree item
					$("ul a.layer-selector", element).before('<span class="lyrBtn"><img src="images/icon_layer.gif" /></span>');
					
					// apply styles	
					
					$("ul", element).css ({'list-style': 'none', "margin-left": "17px"});
					$("ul:first", element).css ({padding : 0});
					$("li", element).css ({display: 'inline'}); // IE 8 problem?
					$("a", element).css ({display: "block", padding: "1px 0 2px 3px", cursor: "pointer"});
					$("li ul", element).css ({ margin: 0, "margin-left": "1em", padding: "0.25em 0", display: "none"}); // "list-style": "none"
					$("li ul li", element).css ({ padding: "1px 0", display: "list-item" /* IE 6 problem */ });
				
					$("li ul li a", element).css ({"padding-top": "0px", "padding-bottom": "1px"});
					$("li ul li ul", element).css({display: "none"});
					$(".tocBtn", element).css({ "float": "left", "margin-left": "-17px" /* move button into left-margin */, "margin-top": "2px", 
						width:"16px", height:"16px",cursor:"pointer"});
					$(".lyrBtn", element).css({ "float": "left", "margin-left": "-17px" /* move button into left-margin */, "margin-top": "2px", 
						width:"16px", height:"16px",cursor:"pointer", "text-align": "center"});
					//$(element).css({ overflow: "auto" }); strange behavior
					
					// read currently loaded layers from map and synchronize catalogue state
					$.each(map.layers, function (index, layer) {
							layerfound = false;
							$.each($("a.layer-selector", element), function (index, layerlink) {
									$layerlink = $(layerlink);
									layermetadata = $layerlink.data("layermetadata");
									if (layermetadata.title == layer.name)
									{
										layermetadata.openlayersid = layer.id;
										layerfound = true;
										// store layer metadata with layer
										layer.layermetadata = layermetadata;
									}
								}
							)
							if (!layerfound)
							{
								// layermetadata not available, generate it
								var layermetadata = { title: layer.name, type: layer.isBaseLayer ? 'baselayer' : 'overlay', openlayersid: layer.id };
								// store layer metadata with layer
								layer.layermetadata = layermetadata;
								// layer exists in map, but not yet in list, so append to root of catalogue tree
								$layerlink = $('<a class="layer-selector" id="layer'+ index + '">' + layer.name + '</a>');
								$layerlink.data("layermetadata", layermetadata);
								$("ul:first", element).append($('<li></li>').append($layerlink));
							}
						}
					)
					// mark currently active baselayer
					$("ul a.layer-selector", element).filter(
						function(index) {return $(this).data("layermetadata").openlayersid == map.baseLayer.id;})
							.siblings(".lyrBtn").html("√&nbsp;");
							
					// register handlers for map addlayer and removelayer events
					map.events.register("preaddlayer", { jqwrapper: this, map: map }, function(e) {
							// display loading-layer icon
							
							$(this.jqwrapper)
								.find("a.layer-selector")
									.filter(function (index) {return ($(this).data("layermetadata").openlayersid == e.layer.id) && ($(this).data("layermetadata").type != 'baselayer')})
										//.siblings(".lyrBtn").html("≈&nbsp;");
										.siblings(".lyrBtn").html('<img src="images/spinner.gif" />');
										
							// register event for display layer-loaded icon
							e.layer.events.register("loadend", { element:this.jqwrapper, layerid: e.layer.id }, function (e) {
								layerid = this.layerid;
								$(this.element)
									.find("a.layer-selector")
										.filter(function (index) {return ($(this).data("layermetadata").openlayersid == layerid) && ($(this).data("layermetadata").type != 'baselayer')})
											//.siblings(".lyrBtn").html("≠&nbsp;");
											.siblings(".lyrBtn").html('<img src="images/icon_layerok.gif" />');
								}
							);
						}
					);
					map.events.register("changebaselayer", { jqwrapper: this, map: map }, function(e) {
							// first reset all base layer icons to default icon, then set current baselayer icon to '√'
							$button = $(this.jqwrapper)
								.find("a.layer-selector")
									.filter(function (index) {return $(this).data("layermetadata").type == "baselayer";})
										.siblings(".lyrBtn").html('<img src="images/icon_layer.gif" />').end()
										.filter (function (index) { return $(this).data("layermetadata").openlayersid == e.layer.id;})
											.siblings(".lyrBtn").html("√&nbsp;");
							
						}
					);
					map.events.register("removelayer", { jqwrapper: this, map: map }, function(e) {
							// layer is being removed from map, so display addlayer icon in front of catalogue tree item
							$layerselector = $(this.jqwrapper).find("a.layer-selector");
							$layerselector.filter(function (index) {layermetadata = $(this).data("layermetadata"); if (!layermetadata) return false; if (!layermetadata.openlayersid) return false; return layermetadata.openlayersid == e.layer.id;})
								.siblings(".lyrBtn").html('<img src="images/icon_layer.gif" />');
							// remove stored openlayers id of currently removed layer from catalogue tree item
							if ($layerselector.data("layermetadata")) {
								$layerselector.data("layermetadata").openlayersid = null;
							}
						}
					);
					
					// register layerlist click handler for map layer toggling
					$("a.layer-selector", element).click(function(e) { _toggleLayer($(this).data("layermetadata"), map); });
					$(".lyrBtn", element).click(function (e) { _toggleLayer($(this).siblings("a.layer-selector").data("layermetadata"), map);});
					
					// add click handlers for non-leaf nodes
					$(element)
						.find(".tocBtn").click( function() { _toggleList(this); } ).end()
						.find(".folder").click( function() { _toggleList(this); } ).end()
					if ($.browser.msie)
					{
						// IE a:hover requires href attribute?
						$("a", element).attr ("href", "javascript:void(0)");
					}
						
				} 
			); // .each element
			break;
		case 'legend':
			settings = $.extend ({
				layerDefs: [{ title: map.baseLayer.name,					
									type: "baselayer",
									groupselector: "#baselayer",
									legendurl: ""
								}]
			}, options || {});
			
			layerDefs = settings.layerDefs;
			$(this).css({ overflow: "auto"});
			
			// do with all elements in the wrapped set..
			$.each(this, function (index, element) {
					// create a container for baselayer legends and a container for overlay legends
					$(element).append('<div class="legendoverlaylayers"></div>');
					$(element).append('<div class="legendbaselayers"></div>');
					// add legend for layers that are already loaded in map
					$.each(map.layers, function (i, layer) {
							if (layer.getVisibility())
							{
								if (!layer.layermetadata)
								{
									// search for layermetadata in layerdefs
									$.each(layerDefs, function (j, layermetadata) {
										if (layer.name == layermetadata.title)
										{	
											layer.layermetadata = layermetadata;
										}
									});
								}
								if (layer.layermetadata)
								{
									$legenditem = $(_LegendHtml (layer.layermetadata.title, layer.layermetadata.legendurl));
									$legenditem.data ("layermetadata", layer.layermetadata);
									$legenditem.data ("openlayersid", layer.id);
									layerfound = true;
								} else {
									// no layer metadata available
									$legenditem = $(_LegendHtml (layer.name, null));
									$legenditem.data ("openlayersid", layer.id);
								}
								if (layer.isBaseLayer)
								{
									$legenditem.addClass ("baselayer");
									$(element).find(".legendbaselayers").append($legenditem);
									// event listener for 'changebaselayer'
									map.events.register("changebaselayer", { jqwrapper: $legenditem, map: map }, function(e) {
											var legendUrl = null;
											if (e.layer.layermetadata)
											{
												legendUrl = e.layer.layermetadata.legendurl;
											}
											this.jqwrapper.data ("openlayersid", e.layer.id);
											// update base layer for this legend item
											_UpdateLegendHtml (this.jqwrapper, e.layer.name, legendUrl);
											// update layer metadata with legend
											
											var newmetadata = new Object();
											newmetadata.legendurl = legendUrl;
											newmetadata.title = e.layer.name;
											newmetadata.openlayersid = e.layer.id;
											this.jqwrapper.data("layermetadata", newmetadata);
											$(element).trigger("legendchanged", [this.jqwrapper]);
										}
									);
								} else {
									// layer is an overlay layer
									$(element).find(".legendoverlaylayers").append($legenditem);
								}
								$(element).trigger("legendadded", [$legenditem]);
							}
						}
					);
					
					// event listener for 'addlayer'
					map.events.register("addlayer", { jqwrapper: this, map: map }, function(e) {
							url = null;
							if (e.layer.layermetadata) {
								url = e.layer.layermetadata.legendurl;
							}
							$legenditem = $(_LegendHtml(e.layer.name, url));
							$legenditem.data("layermetadata", e.layer.layermetadata);
							$legenditem.data("openlayersid", e.layer.id);
							$legenditem.hide();
							$(element).find(".legendoverlaylayers").prepend($legenditem);
							// update legends for current scale
							_UpdateLegends (element, map);
							$(element).trigger("legendadded", [$legenditem]);
							$legenditem.slideDown ();
						}
					);
					// event listener for 'removelayer'
					map.events.register("removelayer", { jqwrapper: this, map: map }, function(e) {
							$layerlegend = $(element).find(".layerlegendcontainer").filter (function (index) { return $(this).data("openlayersid") == e.layer.id; });
							// animate removal
							$layerlegend.slideUp (function() { $layerlegend.remove() });
						}
					);
					
					// event listener for zoomend / zoomend
					map.events.register("moveend", {legendcontainer: this, map: map}, function(e) {
							_UpdateLegends (this.legendcontainer, this.map);
						}
					);
					
					
				}
			); // .each element
			break;
		default:
			alert ("mapwidget: unknown widgettype '" + widgettype + "'");
			break;
	}
	return this;
}

//*****************************************************************************//
//****************************** private functions ****************************//
// draw a scale bar
var _drawScaleBar = function(scalebar, map)
{
	// todo: sphericalmercator: adjust for smaller scale at increasing latitudes
	sbarsettings = scalebar.data("sbarsettings");
	res = map.getResolution(); // units per pixel
	// find nearest multiple of 1, 2, 4, 5 below maxWidth * res
	powerOfTen = Math.floor((Math.log (sbarsettings.maxWidth * res) / Math.log(10)));
	base = Math.floor((sbarsettings.maxWidth * res) / Math.pow(10, powerOfTen));
	base = (base > 5) ? 5 : (base > 4) ? 4 : (base > 2) ? 2 : 1;
	
	scaleLength = base * Math.pow(10, powerOfTen);
	pixelLength = Math.floor(scaleLength / res);
	if (scaleLength < 1)
	{
		message = Math.floor (scaleLength * 100) + " cm";
	} else if (scaleLength < 1000) {
		message = scaleLength + " m";
	} else {
		message = scaleLength / 1000 + " km";
	}
	$(scalebar).html(sbarsettings.labelScale + 
		'<span style="display: inline-block; width: ' + pixelLength + 'px; background-color: #FFA500; height: 5px;"></span>' + message );
		//'<table style="display: inline"><tbody style="display: inline"><tr style="display: inline"><td style="display: inline" width="' +  pixelLength + '" bgcolor="orange" height="5px"></td></tr></tbody></table>' + message );
}

// add layer if not yet in map, else remove layer from map
var _toggleLayer = function(layermetadata, map) 
{
	if (layermetadata.openlayersid)
	{
		// this in an existing OpenLayers layer
		layer = map.getLayer(layermetadata.openlayersid);
		if (layermetadata.type != "baselayer")
		{
			// remove layer
			if (layer)
				map.removeLayer (layer, false); // false param 'setNewBaseLayer'. Is this necessary?
			// layer no longer exists as OpenLayers layer
			layermetadata.openlayersid = null;
			layer = null;
			return;
		}
	}
	// create overlay layer or select different base layer
	switch (layermetadata.type)
	{
		case "wms":
			layer = new OpenLayers.Layer.WMS(layermetadata.title, layermetadata.url, {
				layers: layermetadata.layers,
				transparent: true,
				format: layermetadata.format
			}, layermetadata.layeroptions);
			layermetadata.openlayersid = layer.id;
			// store layer metadata with layer
			layer.layermetadata = layermetadata;
			map.addLayer (layer);
			break;
		case "baselayer":
			if (!layermetadata.openlayersid)
			{
				layers = map.getLayersByName (layermetadata.title);
				if (layers.length > 0)
				{
					layer = layers[0];
					layermetadata.openlayersid = layer.id;
					// store layer metadata with layer
					layer.layermetadata = layermetadata;
				} else {
					message = "";
					$.each (map.layers, function (index, layer) { message += "\n" + layer.name; });
					alert (message + "\nbase layer " + layermetadata.title + " not found");
					return;
				}
			}
			map.setBaseLayer (layer);
			break;
		default:
	}
}

function buildTextArray (xmldoc, selector)
{
	var result = new Array();
	$(xmldoc).find(selector).each (function (index, element) {
		result.push($(element).text());
	});
	return result;
}

function getLayers(data, FilterString) {
	var result = new Array();
	$(data).find(FilterString).each (function (index, layer) {
		var layerinfo = {
			name: $(layer).find(">Name").text(),
			title: $(layer).find(">Title").text(),
			'abstract': $(layer).find(">Abstract").text(),
			styles: function () { // get array of layer styles
					var styleArray = new Array();
					$(layer).find(">Style").each (function(index, style) {
						styleArray.push ({ 
							name: $(style).find("Name").text(), 
							title: $(style).find("Title").text(),
							legendurl: $(style).find("LegendURL>OnlineResource").attr("xlink:href") || 
								$(style).find("LegendURL>OnlineResource").attr("href") // Opera
						});
					});
					return styleArray;
				} (),
			scalehintmin: $(layer).find(">ScaleHint").attr("min"),
			scalehintmax: $(layer).find(">ScaleHint").attr("max"),
			srs: buildTextArray(layer, ">SRS"),
			bbox: function () {
					$llbbox = $(layer).find(">LatLonBoundingBox");
					if ($llbbox.length == 0)
						return null;
					return {
						minx: $llbbox.attr("minx"),
						miny: $llbbox.attr("miny"),
						maxx: $llbbox.attr("maxx"),
						maxy: $llbbox.attr("maxy")
					}
				} (),
			srsboxes: function () {
					var srsresult = new Array();
					$srsbboxes = $(layer).find(">BoundingBox");
					$srsbboxes.each (function (index, bbox) {
						srsresult.push ({
							srs: $(bbox).attr("SRS"), 
							minx: $(bbox).attr("minx"),
							miny: $(bbox).attr("miny"),
							maxx: $(bbox).attr("maxx"),
							maxy: $(bbox).attr("maxy")
						});
					});
					return srsresult;
				} (),
			layers: getLayers(layer, ">Layer") // recurse into sublayers
		};
		result.push(layerinfo);
	});
	return result;
} 

LayerTreeToArray = function (LayerTree)
{
	var result = new Array();
	$.each (LayerTree, function (index, layer)
	{
		result.push (layer);
		$.each(layer.layers, function (index, layer)
		{
			result.push (layer);
			$.each (layer.layers, function (index, layer)
			{
				result.push(layer);
			});
		});
	});
	return result;
}
		
_GetListItemsForList = function ($List) {
	// $(List) points to <ul> element(s)
	// The <ul> may contain <li> elements that should be expanded to multiple <li> elements by a getcapabilities request to WMS provider
	$.each($List, function (index, ul) {
		$.each($(ul).find(">li>a.layer-selector"), function (index, li) {
			layermetadata = $(li).data("layermetadata");
			if (layermetadata && (layermetadata.type == "getcapabilities"))
			{	
				// this li element should be replaced by li elements from getcapabilites
				$(li).html('<img src="images/spinner.gif" />');
				url = layermetadata.url;
				if (url.substring(0,7).toLowerCase() == "http://")
				{
					// todo: take proxy from option, parameter or variable
					url = "./proxy.php?url=" + escape(url);
				}
				$.ajax ( {
					url: url,
					type: 'GET',
					dataType: 'xml', 
					timeout: 5000,
					success: function (data) {
						getmapurl = $(data).find("Capability Request GetMap Get OnlineResource").attr("xlink:href") ||
							$(data).find("Capability Request GetMap Get OnlineResource").attr("href"); // Opera
						getfeatureinfourl = $(data).find("Capability Request GetFeatureInfo Get OnlineResource").attr("xlink:href") ||
							$(data).find("Capability Request GetFeatureInfo Get OnlineResource").attr("href"); // Opera
						getstylesurl = $(data).find("Capability Request GetStyles Get OnlineResource").attr("xlink:href") ||
							$(data).find("Capability Request GetStyles Get OnlineResource").attr("href"); // Opera
						layertree = getLayers(data, "Capability > Layer");
						layers = LayerTreeToArray (layertree);
						var firstLayer = true;
						$.each(layers, function (index, layer) {
							if (layer.name != "") 
							{
								var newlayermetadata = {
									title: layer.title,
									type: "wms",
									url: getmapurl,
									layers: layer.name,
									format: "image/png",
									legendurl: null,
									layeroptions: layermetadata.layeroptions,
									getfeatureinfourl: getfeatureinfourl,
									querylayers: layer.name
								};
								if (layer.styles.length > 0 && layer.styles[0].legendurl != null)
								{
									newlayermetadata.legendurl = layer.styles[0].legendurl;
								}
								if (firstLayer)
								{
									$(li).text(layer.title);
									$(li).data("layermetadata", newlayermetadata);
									firstLayer = false;
								} else {
									$newelement = $(li).parent().clone(true);
									$newelement.find("a").text(layer.title).data("layermetadata", newlayermetadata);
									$newelement.appendTo($(li).parent().parent());
								}
							}
						});
					},
					error: function (xhr, statusstring, ex) {
						$(li).text("? GetCapabilities mislukt");
					}
				});				
			}
		});
	});
}

// function for sliding list elements open and closed
_toggleList = function (clickedElement) {
	var $Btn = $(clickedElement);
	if ($Btn.is(".folder"))
	{
		$Btn = $(clickedElement).siblings("span");
	}
	var $List = $(clickedElement).siblings("ul");
	if ($List.is(":visible")) {
		$List
			.css("width", $List.innerWidth())
			.slideUp('fast', function() {$(clickedElement).css("width","auto");})
		;
		//$Btn.css({ backgroundImage: 'url("../jquery/img/icon_tree_on.gif")' });
		$Btn.html('►&nbsp;');
							  
	}
	else {
		_GetListItemsForList ($List); // first check if new items should be added
		$List.slideDown('fast');
		//$Btn.css({ backgroundImage: 'url("../jquery/img/icon_tree_off.gif")' });
		$Btn.html('▼&nbsp;');
	}
};

// return html code for a layer legend
// this is the default function, a custom function can be passed through the legend options
_LegendHtml = function (title, url) 
{
	legendhtml = '<div class="layerlegendcontainer"><div class="layerlegendheader"><span class="layerlegendtitle">'
					+ title + '</span>&nbsp;</div>' + 
					'<div class="layerlegendcontent">';
	if (!url || url.length == 0)
	{
		url = "images/nolegend.png"
	}
	legendhtml += '<img src="'+ url + '" title="legend for layer" />'
	legendhtml += "</div></div>";
	return legendhtml;
}

_UpdateLegendHtml = function ($element, title, url)
{
	$element.find(".layerlegendtitle").text(title);
	$img = $element.find("img");
	if ($img.length > 0)
		$img[0].src = url;
}

// update legend texts for new scale for all overlay layers
_UpdateLegends = function (legendcontainer, map)
{
	$.each($(legendcontainer).find(".layerlegendcontainer"), function (index, legend) {
			overrideUrl = null;
			layerid = $(legend).data("openlayersid");
			if (layerid) {
				layer = map.getLayer (layerid);
				if (layer)
				{
					if (layer.maxResolution < map.getResolution()) {
						overrideUrl = "images/zoomin.png";
					} else if (layer.minResolution > map.getResolution()) {
						overrideUrl = "images/zoomout.png";
					}
				}
			} else {
				layer = { name: "unknown" };
			}
			layermetadata = $(legend).data("layermetadata");
			if (!layermetadata)	{
				layermetadata = { title: layer.name, legendurl: "images/nolegend.png" };
			}
			_UpdateLegendHtml ($(legend), layermetadata.title, overrideUrl? overrideUrl : layermetadata.legendurl);
		}
	);
}
					
})(jQuery);